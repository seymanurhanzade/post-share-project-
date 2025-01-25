using Microsoft.AspNetCore.Mvc;
using react.server.ApplicationContext;
using react.server.DataManagement.server.data.Abstract;
using react.server.Models;
using server.business.Abstract;
using server.entitys;

namespace react.server.DataManagement.server.business.Concrete
{
    public class InstantShareManager : IInstantShareService
    {
        private readonly IInstantShareRepository _instantShareRepository;
        private readonly IdentityContext _context;

        public InstantShareManager(IInstantShareRepository instantShareRepository,IdentityContext context){
            _instantShareRepository = instantShareRepository;
            _context= context;
        }

        public Task<IActionResult> AddToCommunt(CommuntModel model)
        {
            return _instantShareRepository.AddToCommunt(model);
        }

        public Task<IActionResult> AddToShare(InstantShareModel model)
        {
            return _instantShareRepository.AddToShare(model);
        }

        public Task<List<HomePostList>> AnasayfaGonderileri(string UserId)
        {
            return _instantShareRepository.AnasayfaGonderileri(UserId);
        }

        public void Create(InstantShare entity)
        {
            _instantShareRepository.Create(entity);
        }

        public void Delete(InstantShare entity)
        {
            _instantShareRepository.Delete(entity);
        }

        public async Task DeletePost(int id)
        {
            var data = GetById(id);
            var dltData = new DeletePost{
                Share= data.Share,
                Time= DateTime.Now,
                UserId = data.UserId,
            };
            await _context.DeletePost.AddAsync(dltData);
            
            _instantShareRepository.Delete(data);
            _context?.SaveChangesAsync();
        }

        public InstantShare GetById(int id)
        {
            return _instantShareRepository.GetById(id);
        }

        public Task<List<GetPostModel>> GetCommunt(int postId)
        {
            return _instantShareRepository.GetCommunt(postId);
        }

        public Task<bool> IsPostLikedByUser(int postId, string userId)
        {
            return _instantShareRepository.IsPostLikedByUser(postId, userId);
        }

        public Task<IActionResult> LikeAdd(LikeModel model)
        {
            return _instantShareRepository.LikeAdd(model);
        }
        // public Task<List<LikeModelPostIdList>> LikeGruop(string UserId)
        // {
        //     return _instantShareRepository.LikeGruop(UserId);
        // }

        public Task<List<InstantShareModelList>> PostDetails(int postId,string UserId)
        {
            return _instantShareRepository.PostDetails(postId,UserId);
        }

        public Task<List<InstantShareModelList>> TEAnasayfaGonderileri(string UserId)
        {
            return _instantShareRepository.TEAnasayfaGonderileri(UserId);
        }
    }
}