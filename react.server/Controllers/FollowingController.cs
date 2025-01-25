using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using react.server.DataManagement.server.business.Abstract;
using react.server.Models;

namespace react.server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FollowingController:ControllerBase
    {
        private readonly IFollowingService _followingService;
        public FollowingController(IFollowingService followingService){
            _followingService = followingService;
        }

        [HttpPost("follower-add")]
        public async Task<IActionResult> FollowerAdd(FollowModel model){
            
            return await _followingService.FollowerAdd(model);
        }

        [HttpPost("following-control")]
        public async Task<IActionResult> FollowingControl(FollowModel model){
            return await _followingService.FollowingControl(model);
        }
        [HttpGet("takipedilenler/{userId}")]
        public async Task<List<FollowingList>> TakipList(string userId){
            return await _followingService.TakipList(userId);
        }
        [HttpGet("takipciler/{userId}")]
        public async Task<List<FollowUserModel>> Takipciler(string userId){
            return await _followingService.Takipciler(userId);
        }
        
    }
}