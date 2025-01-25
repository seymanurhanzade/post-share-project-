using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using react.server.ApplicationContext;
using react.server.DataManagement.server.data.Abstract;
using react.server.Models;
using server.data.Concrate;

namespace react.server.DataManagement.server.data.Concrate
{
    public class ConcrateUserRepository:ConcrateRepository <Users, IdentityContext>, IUserRepository
    {
        private readonly IdentityContext _context;
        private readonly UserManager<Users> _userManager;
        private readonly IInstantShareRepository _instantShareRepository;
        private readonly IWebHostEnvironment _env;
        public ConcrateUserRepository(IdentityContext context, UserManager<Users> userManager, IInstantShareRepository instantShareRepository, IWebHostEnvironment env):base(context)
        {
            _context = context;
            _userManager = userManager;
            _instantShareRepository = instantShareRepository;
            _env = env;
        }

        public async Task<List<GetUserModel>> GetUserProfile(string userId)
        {
            var user= await _userManager.FindByIdAsync(userId);

            var data = await _context.Users.Where(u=>u.Id== user.Id).Select(u=> new GetUserModel{
                UserId=user.Id,
                FullName=user.FullName,
                AtUserName= user.AtUserName,
                UserImages=user.Image,
                Email=user.Email,
                Password= user.Password,
                TakipEdilenSayisi=user.TotalFollower,
                TakipciSayisi=user.TotalFollowed
            }).ToListAsync();
            return data;
        }

        public async Task<List<GetUserModel>> GetUsers()
        {
            var data =await _context.Users.Select(x=> new GetUserModel{
                UserId =x.Id,
                AtUserName= x.AtUserName,
                FullName= x.FullName,
                UserImages=x.Image
            }).ToListAsync();
           
            return data;
        }

        public async Task<IActionResult> UserImage(IFormFile file, string userId)
        {
            Console.WriteLine("File Name-------------- "+ file + "------"+userId);
            try
            {
                
                var user = await _context.Users.FirstOrDefaultAsync(x=>x.Id== userId);
                Console.WriteLine("-------------- "+ "------"+user?.Email);
                if (user == null)
                {
                    Console.WriteLine("Kullanıcı bulunamadı.");
                    return new BadRequestObjectResult("kullanıcı boş olamaz.");
                }

                if (file != null && file.Length > 0)
                {
                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                    Console.WriteLine("File Name-------------- "+ fileName);
                    // var filePath = Path.Combine(_env.ContentRootPath, "react.app", "public", "Images", fileName);
                    var filePath = Path.Combine(@"C:\Users\seyma\Desktop\PR\ReactProjects\InstantShareProject\react.app\public\Images", fileName);
                    

                    // Dosya dizinini oluşturma
                    if (!Directory.Exists(Path.GetDirectoryName(filePath)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                    }

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    var ent = user.Image = fileName;
                    await _userManager.UpdateAsync(user);
                    return new OkObjectResult(ent);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata: {ex.Message}");
                Console.WriteLine(ex.ToString());
                return new BadRequestObjectResult("hata: "+ ex.Message);
            }
            return new BadRequestObjectResult("");
        }


        public async Task<List<UserProfileModel>> UserProfilePost(string userId)
        {
            Console.WriteLine("UserId: "+ userId);
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return null; 
            }
            var likePost= await _context.LikesTables.Where(x=>x.UserId==user.Id)
                .Join(_context.InstantShares, u=>u.PostId, p=>p.Id, (u,p) => new
                {
                    PostId= p.Id,
                    share= p.Share,
                    Time= p.Time.ToString(),
                    count= p.Count,
                    commentCount= p.CommuntCount,
                    PostUserId= p.UserId
                }).Join(_context.Users, x=>x.PostUserId, p=>p.Id, (x,p)=> new 
                {
                    likePostUserId=x.PostUserId,
                    likePostIds= x.PostId,
                    likePostShare= x.share,
                    likePostTime= x.Time.ToString(),
                    likePostIdUser= x.PostUserId,
                    likePostAtUserName= p.AtUserName,
                    likePostFullName= p.FullName,
                    likePostUserImages=p.Image,
                    likePostLikesCount= x.count,
                    likePostCommentCount= x.commentCount,
                }).ToListAsync();
                
            
            var data = await _context.Users
                .Join(
                    _context.InstantShares,
                    u => u.Id,
                    p => p.UserId,
                    (u, p) => new 
                    {
                        UserId = u.Id,
                        atUserName = u.AtUserName,
                        fullName = u.FullName,
                        Username= u.UserName,
                        Images=u.Image,
                        shareId=p.Id,
                        share = p.Share,
                        Time = p.Time.ToString(),
                        count = p.Count,
                        commentCount=p.CommuntCount,
                    }
                )
                .Where(joined => joined.UserId == user.Id)
                .ToListAsync();


            var groupedData = data
                .GroupBy(d => new { d.UserId,  d.fullName,d.Username,d.Images }) 
                .Select(g => new UserProfileModel
                {
                    userId = g.Key.UserId,
                    FullName= g.Key.fullName,
                    UserName=g.Key.Username,
                    Image=g.Key.Images,
                    UserProfileList = g.Select(item => new UserProfileModelList
                    {
                        Id=item.shareId,
                        AtUserName = item.atUserName,
                        fullName = item.fullName,
                        Image=item.Images,
                        share = item.share,
                        Time = item.Time,
                        Count= item.count,
                        CommentCount=item.commentCount,
                    }).ToList()
                    ,LikePosts = likePost.Select(lp => new LikeModelList{
                        UserId=lp.likePostIdUser,
                        PostId = lp.likePostIds,
                        AtUserName = lp.likePostAtUserName,
                        fullName = lp.likePostFullName,
                        Image=lp.likePostUserImages,
                        share = lp.likePostShare,
                        Time = lp.likePostTime,
                        Count=lp.likePostLikesCount,
                        CommentCount=lp.likePostCommentCount,
                        }).ToList()
                    }).ToList();

                Console.WriteLine($"Data Count: {data.Count}");
                Console.WriteLine($"Like Post Count: {likePost.Count}");


                return groupedData;
        }
    
    
    
        public async Task<IActionResult> ProfileEdit([FromForm] ProfileEditModel model)
        {
            Console.WriteLine("File Name-------------- "+ model.Images + "------"+model.UserId);
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x=>x.Id== model.UserId);
                Console.WriteLine("-------------- "+ "------"+user.Email);
                if (user == null)
                {
                    Console.WriteLine("Kullanıcı bulunamadı.");
                    return new BadRequestObjectResult("kullanıcı boş olamaz.");
                }

                if (model.Images != null && model.Images.Length > 0)
                {
                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(model.Images.FileName)}";
                    Console.WriteLine("File Name-------------- "+ fileName);
                    // var filePath = Path.Combine(_env.ContentRootPath, "react.app", "public", "Images", fileName);
                    var filePath = Path.Combine(@"C:\Users\seyma\Desktop\PR\ReactProjects\InstantShareProject\react.app\public\Images", fileName);
                    

                    // Dosya dizinini oluşturma
                    if (!Directory.Exists(Path.GetDirectoryName(filePath)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                    }

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.Images.CopyToAsync(stream);
                    }
                    var ent = user.Image = fileName;

                    if(model.FullName==null){
                        user.FullName=user.FullName;
                    }else{
                        user.FullName= model.FullName;
                    }
                    
                    await _userManager.UpdateAsync(user);
                    return new OkObjectResult(ent);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata: {ex.Message}");
                Console.WriteLine(ex.ToString());
                return new BadRequestObjectResult("hata: "+ ex.Message);
            }
            return new BadRequestObjectResult("");
        }
    }
}