using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using react.server.ApplicationContext;
using react.server.Models;
using server.entitys;

namespace server.business.Abstract
{
    public interface IInstantShareService
    {
        Task<IActionResult> AddToShare(InstantShareModel model);
        Task<List<InstantShareModelList>> PostDetails(int postId,string UserId);
        InstantShare GetById(int id);
        Task<List<HomePostList>> AnasayfaGonderileri(string UserId);
        Task<List<InstantShareModelList>> TEAnasayfaGonderileri(string UserId);
        // Task<List<LikeModelPostIdList>> LikeGruop(string UserId);
        Task<IActionResult> LikeAdd(LikeModel model);
        Task<IActionResult> AddToCommunt(CommuntModel model);
        Task<List<GetPostModel>> GetCommunt(int postId);
        void Delete(InstantShare entity);
        Task DeletePost(int id);
        void Create(InstantShare entity);
        Task<bool> IsPostLikedByUser(int postId,string UserId);

    }
}