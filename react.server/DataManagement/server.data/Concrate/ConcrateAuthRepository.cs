using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using react.server.ApplicationContext;
using react.server.DataManagement.server.entitys;
using react.server.Models;
using server.data.Abstract;

namespace server.data.Concrate
{
    public class ConcrateAuthRepository: ConcrateRepository<Users, IdentityContext>, IAuthRepository
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;
        private readonly IdentityContext _context;
        

        public ConcrateAuthRepository(IConfiguration configuration,SignInManager<Users> signInManager, UserManager<Users> userManager,IdentityContext context):base(context){
            _configuration = configuration;
            _signInManager = signInManager;
            _userManager = userManager;
            _context= context;
        }


        public async Task Register(UserRegisterDTO model)
        {
            var user = new Users
            {
                FullName= model.FullName,
                UserName =model.UserName,
                AtUserName="@"+  model.UserName,
                Email = model.Email,
                Password = model.Password,
                Image= "/user.jpg"
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            
            if(result.Succeeded)
            {
                
                var userId = await _userManager.FindByEmailAsync(user.Email);
                Console.WriteLine("userId: ", userId.Id);

                var FollowedUser = await _context.FollowedTable.FirstOrDefaultAsync(x=>x.UserId == userId.Id);
                var FollowerUser = await _context.FollowerTable.FirstOrDefaultAsync(x=>x.UserId == userId.Id);
                if(FollowedUser == null){
                    var followedTable = new FollowedTable{
                        UserId = userId.Id,
                        FollowedUserId=null,
                    };
                    _context.FollowedTable.Add(followedTable);
                    await _context.SaveChangesAsync();

                }
                if(FollowerUser==null){
                    var followerTable= new FollowerTable{
                        UserId= user.Id,
                        FollowerUserId=null
                    };
                    _context.FollowerTable.Add(followerTable);
                    await _context.SaveChangesAsync();
                }else{
                    Console.WriteLine("Kullanıcı zaten mevcut.");
                }
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine($"Hata: {error.Description}");
                }
                throw new Exception("Kullanıcı kaydı başarısız.");
            }
        
        }


        public async Task<string> Login(UserLoginDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            var result = await _signInManager.CheckPasswordSignInAsync(user,model.Password,false);

            if(result.Succeeded){
                var token = await GenerateJwtToken(user);
                Console.WriteLine("Token: "+token);
                return token;
            }
            else{
                throw new Exception("Kullanıcı girişi başarısız.");
            }
        }   

        private async Task<string> GenerateJwtToken(Users user){
            var tokenHandler = new JwtSecurityTokenHandler();
            var key= Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Secret").Value);

            var tokenDescriptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(new[] { 
                    new Claim(ClaimTypes.NameIdentifier,user.Id),
                    new Claim(ClaimTypes.Email, user.Email),
                    }),
                    Expires =DateTime.UtcNow.AddMinutes(50),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}