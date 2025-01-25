using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using react.server.ApplicationContext;
using react.server.DataManagement.server.data.Abstract;
using react.server.Models;
using server.business.Abstract;
using server.entitys;

namespace react.server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShareController:ControllerBase
    {
        private readonly IInstantShareService _instantShareService;

        public ShareController(IInstantShareService instantShareService) {
            _instantShareService = instantShareService;
        }
        
        [HttpPost("share-add")]
        public async Task<IActionResult> ShareAdd(InstantShareModel model){
             return await _instantShareService.AddToShare(model);
        }
        [HttpGet("home/{userId}")]
        public  Task<List<HomePostList>> GetInstantUserId(string userId)
        {
            return _instantShareService.AnasayfaGonderileri(userId);
        }
        [HttpGet("takipedilenler-home/{userId}")]
        public  Task<List<InstantShareModelList>> TakipEdilenler(string userId)
        {
            return _instantShareService.TEAnasayfaGonderileri(userId);
        }

        [HttpPost("like-add")]
        public async Task<IActionResult> LikeAdd(LikeModel model){
            return await _instantShareService.LikeAdd(model);
            
        }
        
        // [HttpGet("getpostIdLike")]
        // public async Task<List<LikeModelPostIdList>> GetLikePost(string userId)
        // {
        //     return await _instantShareService.LikeGruop(userId);
        // }

        [HttpPost("add-communt")]
        public async Task<IActionResult> AddCommunt(CommuntModel model){
            return await _instantShareService.AddToCommunt(model);
        }

        [HttpGet("get-communt/{postId}")]
        public async Task<List<GetPostModel>> GetComment(int postId){
            return await _instantShareService.GetCommunt(postId);
        }

        [HttpGet("post-detail/{postId}/{UserId}")]
        public async Task<List<InstantShareModelList>> PostDetails(int postId,string UserId){
            return await _instantShareService.PostDetails(postId,UserId);
        }

        [HttpDelete("Delete")]
        public IActionResult PostDelete([FromQuery] int id){
            _instantShareService.DeletePost(id);
            return Ok();
        }

        [HttpGet("like-post")]
        public async Task<bool> IsPostLikedByUser([FromQuery] int postId, [FromQuery] string userId)
        {
            Console.WriteLine($"Received Request: postId={postId}, userId={userId}");
            return await _instantShareService.IsPostLikedByUser(postId, userId);
        }
    }
}