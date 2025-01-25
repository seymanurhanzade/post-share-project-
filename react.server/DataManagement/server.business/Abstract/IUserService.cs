using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using react.server.ApplicationContext;
using react.server.Models;

namespace react.server.DataManagement.server.business.Abstract
{
    public interface IUserService
    {
        Task<List<UserProfileModel>> UserProfilePost(string userId);
        Task<List<GetUserModel>> GetUsers();
        Task<List<GetUserModel>> GetUserProfile(string userId);
        Users GetById(int id);
        void Update(Users entity);
        Task<IActionResult> UserImage(IFormFile file, string userId);
        Task<IActionResult> ProfileEdit(ProfileEditModel model);

    }
}