using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using react.server.ApplicationContext;
using react.server.Models;
using server.data.Abstract;
using server.entitys;

namespace react.server.DataManagement.server.data.Abstract
{
    public interface IInstantShareRepository: IRepository<InstantShare>
    {
        Task<IActionResult> AddToShare(InstantShareModel model);
        Task<List<InstantShareModelList>> PostDetails(int postId,string UserId);
        Task<List<HomePostList>> AnasayfaGonderileri(string UserId);
        Task<List<InstantShareModelList>> TEAnasayfaGonderileri(string UserId);
        // Task<List<LikeModelPostIdList>> LikeGruop(string UserId);
        Task<IActionResult> LikeAdd(LikeModel model);
        Task<IActionResult> AddToCommunt(CommuntModel model);
        Task<List<GetPostModel>> GetCommunt(int postId);
        Task<bool> IsPostLikedByUser(int postId, string userId);
        // Task<List<GetUserModel>> GetUsers();
        // Task<List<UserProfileModel>> FilterUserProfile(string userId);

    }
}