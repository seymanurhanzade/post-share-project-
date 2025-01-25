using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using react.server.ApplicationContext;
using react.server.DataManagement.server.business.Abstract;
using react.server.DataManagement.server.data.Abstract;
using react.server.Models;

namespace react.server.DataManagement.server.business.Concrete
{
    public class UserManager :IUserService
    {
        private readonly IUserRepository _userRepository;
        

        public UserManager(IUserRepository userRepository){
            _userRepository = userRepository;
        }

        public Users GetById(int id)
        {
            return _userRepository.GetById(id);
        }

        public Task<List<GetUserModel>> GetUserProfile(string userId)
        {
            return _userRepository.GetUserProfile(userId);
        }

        public Task<List<GetUserModel>> GetUsers()
        {
            return _userRepository.GetUsers();
        }

        public async Task<IActionResult> ProfileEdit([FromForm] ProfileEditModel model)
        {
            return await _userRepository.ProfileEdit(model);
        }

        public void Update(Users entity)
        {
            _userRepository.Update(entity);
        }

        public async Task<IActionResult> UserImage(IFormFile file, string userId)
        {
            return await _userRepository.UserImage(file,userId);
        }

        public Task<List<UserProfileModel>> UserProfilePost(string userId)
        {
            return _userRepository.UserProfilePost(userId);
        }
    }
}