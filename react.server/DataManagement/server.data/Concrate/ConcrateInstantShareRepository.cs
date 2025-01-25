using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using react.server.ApplicationContext;
using react.server.DataManagement.server.data.Abstract;
using react.server.DataManagement.server.entitys;
using react.server.Models;
using server.data.Concrate;
using server.entitys;

namespace react.server.DataManagement.server.data.Concrate
{
    public class ConcrateInstantShareRepository : ConcrateRepository<InstantShare, IdentityContext>, IInstantShareRepository
    {
        private readonly IdentityContext _context;
        private readonly UserManager<Users> _userManager;

        public ConcrateInstantShareRepository(IdentityContext context, UserManager<Users> userManager) :base(context)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> AddToShare(InstantShareModel model)
        {
            if (model == null)
            {
                return new BadRequestObjectResult("Model boş olamaz.");
            }

            if (string.IsNullOrEmpty(model.Share))
            {
                return new BadRequestObjectResult("Paylaşım içeriği boş olamaz.");
            }
            
            var newShare = new InstantShare
            {
                Share = model.Share,
                Time = DateTime.Now,
                UserId = model.userId,
                Count=0
            };

            try
            {
                await _context.InstantShares.AddAsync(newShare);
                await _context.SaveChangesAsync();

                
                return new OkObjectResult(newShare);
            }
            catch (Exception ex)
            {
                return new ObjectResult($"Hata: {ex.Message}") { StatusCode = 500 };
            }
        }

        public async Task<IActionResult> AddToCommunt(CommuntModel model)
        {
            if(model==null){
                return new BadRequestObjectResult("Model boş olamaz.");
            }
            var communt = new PostCommunt{
                UserId= model.UserId,
                PostId= model.PostId,
                Communt= model.Communt,
                Time= DateTime.Now
            };
            try{
                var commentCount= await _context.InstantShares.FirstOrDefaultAsync(x=>x.Id== model.PostId);
                commentCount.CommuntCount ++;
                await _context.PostCommunts.AddAsync(communt);
                await _context.SaveChangesAsync();

                return new OkObjectResult(communt);
            
            }catch(Exception e){
                return new ObjectResult("Hata: " + e.Message);
            }
        }
        public async Task<List<HomePostList>> AnasayfaGonderileri(string UserId)
        {   

            var entity =await _context.Users.Join(_context.InstantShares, u=>u.Id, p=>p.UserId,
            (u,p)=> new InstantShareModelList{
                Id=p.Id,
                userId= p.UserId,
                fullName= u.FullName,
                AtUserName= u.AtUserName,
                Image=u.Image,
                share= p.Share,
                Time = p.Time.ToString(),
                Count= p.Count,
                CommentCount=p.CommuntCount,
            }
            ).ToListAsync();

            var users= await _context.FollowerTable.Where(x=>x.UserId==UserId).ToListAsync();
            var Tentity = await _context.Users
                    .Where(u => users.Select(user => user.FollowerUserId).Contains(u.Id))
                    .Join(_context.InstantShares, u=>u.Id, p=>p.UserId,
                (u,p)=> new InstantShareModelList{
                    Id=p.Id,
                    userId= p.UserId,
                    fullName= u.FullName,
                    AtUserName= u.AtUserName,
                    Image=u.Image,
                    share= p.Share,
                    Time = p.Time.ToString(),
                    Count= p.Count,
                    CommentCount=p.CommuntCount,
                }).ToListAsync();

            var list= new List<HomePostList>{
                new HomePostList{
                    HomePostLists= entity,
                    TakipEdilenlerPostLists=Tentity
                }
            };
            return list;
        }

        public async Task<IActionResult> LikeAdd(LikeModel model)
        {
            try
            {
                var begeniVarmi = await _context.LikesTables
                    .FirstOrDefaultAsync(l => l.PostId == model.PostId && l.UserId == model.UserId);

                if (begeniVarmi != null)
                {
                    try{
                        _context.LikesTables.Remove(begeniVarmi);
                        var post = await _context.InstantShares.FirstOrDefaultAsync(x => x.Id == model.PostId);
                        if (post != null && post.Count > 0) 
                        {
                            post.Count--;
                            _context.InstantShares.Update(post);
                        }
                        var ent= await _context.SaveChangesAsync();
                        return new OkObjectResult(ent);
                    }catch(Exception ex){
                       return new OkObjectResult($"HataS: "+ ex.Message) { StatusCode = 500 }; 
                    }                   
                }else{
                    var like = new LikesTable
                    {
                        PostId = model.PostId,
                        UserId = model.UserId
                    };

                    await _context.LikesTables.AddAsync(like);
                    await _context.SaveChangesAsync();

                    var newPost = await _context.InstantShares.FirstOrDefaultAsync(x => x.Id == model.PostId);
                    if (newPost != null)
                    {
                        newPost.Count++;
                        _context.InstantShares.Update(newPost);
                        await _context.SaveChangesAsync();
                    }
                    return new OkObjectResult(like); 
                }
            }
            catch (Exception ex)
            {
                return new ObjectResult($"Hata: {ex.Message}") { StatusCode = 500 };
            }
        }

        public async Task<bool> IsPostLikedByUser(int postId, string userId)
        {
                var like = await _context.LikesTables
                .FirstOrDefaultAsync(l => l.UserId == userId && l.PostId == postId);
            if (like == null)
            {
                return false;  
            }
            
            return true;  
        }
        public async Task<List<GetPostModel>> GetCommunt(int postId)
        {
           
            var communts= await _context.InstantShares
                .Join(_context.PostCommunts, i=>i.Id, p=>p.PostId, (i,p)=> new {
                    PostId=i.Id,
                    PostShare= i.Share,
                    PostTime= i.Time.ToString(),
                    PostCount= i.Count,
                    PostUserId= i.UserId,
                    CommuntId= p.Id,
                    CommuntPostId= p.PostId,
                    CommuntUserId= p.UserId,
                    CommuntPost = p.Communt,
                    CommentTime= p.Time.ToString(),

                })
                .Join(_context.Users, combined=>combined.PostUserId, p=>p.Id, (combined,p)=> new {
                    combined,
                    UserID= p.Id,
                    UserFullName=p.FullName,
                    UserAtUserName=p.AtUserName,
                    Images= p.Image
                }).Join(_context.Users, i=>i.combined.CommuntUserId, p=>p.Id, (i,p)=> new {
                    i.combined,
                    UserID= p.Id,
                    i.UserAtUserName,
                    i.UserFullName,
                    i.Images,
                    CommentUserFullName=p.FullName,
                    CommentAtUserName= p.AtUserName,
                    CommentUserImages= p.Image
                })
                .Where(x=>x.combined.CommuntPostId== postId)
                .GroupBy(i=> new {i.combined.PostId, i.combined.PostShare, i.combined.PostTime,i.combined.PostCount,i.UserFullName,i.UserAtUserName,i.Images})
                .Select(x=> new GetPostModel{
                    Id= x.Key.PostId,
                    Share= x.Key.PostShare,
                    Time= x.Key.PostTime,
                    Count=x.Key.PostCount,
                    FullName=x.Key.UserFullName,
                    AtUserName=x.Key.UserAtUserName,
                    Images=x.Key.Images,
                    GetPostModelList= x.Select(j=> new GetCommuntModel{
                        Id= j.combined.CommuntId,
                        Communt= j.combined.CommuntPost,
                        PostId= j.combined.CommuntPostId,
                        UserId= j.combined.CommuntUserId,
                        FullName=j.CommentUserFullName,
                        AtUserName=j.CommentAtUserName,
                        Images=j.CommentUserImages,
                        Time= j.combined.CommentTime,
                    }).ToList()

                }).ToListAsync();
            return communts;
            
        }

        public async Task<List<InstantShareModelList>> PostDetails(int postId,string UserId)
        {
            var entity = await _context.Users.Join(_context.InstantShares, i=>i.Id, p=>p.UserId,(i,p)=> new InstantShareModelList{
                Id= p.Id,
                userId=i.Id,
                AtUserName= i.AtUserName,
                fullName= i.FullName,
                share= p.Share,
                Image= i.Image,
                Time=p.Time.ToString(),
                Count=p.Count,
                CommentCount=p.CommuntCount,
            }).Where(x=>x.Id==postId).ToListAsync();
            return entity;
        }

        public async Task<List<InstantShareModelList>> TEAnasayfaGonderileri(string UserId)
        {
            try{
                var users= await _context.FollowerTable.Where(x=>x.UserId==UserId).ToListAsync();


                var entity = await _context.Users
                    .Where(u => users.Select(user => user.FollowerUserId).Contains(u.Id))
                    .Join(_context.InstantShares, u=>u.Id, p=>p.UserId,
                (u,p)=> new InstantShareModelList{
                    Id=p.Id,
                    userId= p.UserId,
                    fullName= u.FullName,
                    AtUserName= u.AtUserName,
                    Image=u.Image,
                    share= p.Share,
                    Time = p.Time.ToString(),
                    Count= p.Count,
                    CommentCount=p.CommuntCount,
                }).ToListAsync();

                return entity;
            }catch(Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return new List<InstantShareModelList>(); // Boş bir liste döndür
            }     
        }
        
    }
}
