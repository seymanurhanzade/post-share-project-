using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using react.server.DataManagement.server.business.Abstract;
using react.server.DataManagement.server.data.Abstract;
using react.server.Models;

namespace react.server.DataManagement.server.business.Concrete
{
    public class FollowingManager : IFollowingService
    {
        private readonly IFollowingRepository _followingRepository;

        public FollowingManager(IFollowingRepository followingRepository){
            _followingRepository = followingRepository;
        }
        public Task<IActionResult> FollowerAdd(FollowModel model)
        {
            return _followingRepository.FollowerAdd(model);
        }

        public Task<IActionResult> FollowingControl(FollowModel model)
        {
            return _followingRepository.FollowingControl(model);   
        }

        public Task<List<FollowUserModel>> Takipciler(string userId)
        {
            return _followingRepository.Takipciler(userId);
        }

        public Task<List<FollowingList>> TakipList(string userId)
        {
            return _followingRepository.TakipList(userId);
        }
    }
}