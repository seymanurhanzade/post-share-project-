using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using react.server.ApplicationContext;
using react.server.DataManagement.server.business.Abstract;
using react.server.Models;

namespace react.server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController: ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService ) {
            _userService = userService;
        }

        [HttpGet("GetUsers")]
        public  Task<List<GetUserModel>> GetUsers(){
            return _userService.GetUsers();
        }
        [HttpGet("profile/{userId}")]
        public async Task<ActionResult<UserProfileModel>> UserProfile(string userId)
        {
            var userProfile = await _userService.GetUserProfile(userId);
            return Ok(userProfile);
        }
        
        [HttpGet("user-profile/{userId}")]
        public async Task<ActionResult<UserProfileModel>> GetUserProfile(string userId)
        {
            var userProfile = await _userService.UserProfilePost(userId);
            return Ok(userProfile);
        }
        [HttpGet("search-profile/{userId}")]
        public async Task<ActionResult<UserProfileModel>> SearchUserProfile(string userId)
        {
            var userProfile = await _userService.UserProfilePost(userId);
            return Ok(userProfile);
        }

        [HttpPost]
        public async Task<IActionResult> UserImage(IFormFile file,string userId){
            if(file !=null && userId !=null){
                 return await _userService.UserImage(file,userId);
            }else{
                return BadRequest("File and userId are required");
            }
            
        }
        [HttpPost("edit-profile")]
        public async Task<IActionResult> ProfileEdit([FromForm] ProfileEditModel model){
            Console.WriteLine($"Gelen FullName: {model.FullName}");
            Console.WriteLine($"Gelen UserId: {model.UserId}");
            Console.WriteLine($"Gelen Dosya: {model.Images?.FileName}");

            if(model.Images !=null && model.UserId !=null){
                return await _userService.ProfileEdit(model);
            }else{
                return BadRequest("File and userId are required");
            }
            
        }

    }
}