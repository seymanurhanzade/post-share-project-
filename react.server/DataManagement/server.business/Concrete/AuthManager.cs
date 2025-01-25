using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using react.server.ApplicationContext;
using react.server.Models;
using server.business.Abstract;
using server.data.Abstract;
using server.data.Concrate;

namespace server.business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly UserManager<Users> _userManager;

        public AuthManager(IAuthRepository authRepository, UserManager<Users> userManager){
            _authRepository = authRepository;
            _userManager = userManager;

        }
        public async Task<ILoginToken> Login(UserLoginDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            var token =await _authRepository.Login(model);
            Console.WriteLine("Token: "+ token+ " UserName: " + user.UserName+ "UserId: " + user.Id);
            if(token==null){
                Console.WriteLine("Boş token");
            }
            return new ILoginToken{
                Token = token,
                AtUserName= user.AtUserName,
                FullName= user.FullName,
                UserId= user.Id,
                UserImages= user.Image
            };
            
        }

        public async Task Register(UserRegisterDTO model)
        {  
            await _authRepository.Register(model);
        }
    }
}