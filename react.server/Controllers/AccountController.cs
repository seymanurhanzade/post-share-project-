using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using react.server.ApplicationContext;
using react.server.Models;
using server.business.Abstract;

namespace react.server.Controllers
{   
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController:ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly SignInManager<Users> _signInManager;
        public AccountController(IAuthService authService,SignInManager<Users> signInManager){
            _authService = authService;
            _signInManager = signInManager;
        }
        
        [HttpPost("register")]
        public async Task<ActionResult> Register(UserRegisterDTO model)
        {
            await _authService.Register(model);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDTO model)
        {
            var res = await _authService.Login(model);
            return Ok(res);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout(){
            await _signInManager.SignOutAsync();
            return Ok();
        }
    }
}