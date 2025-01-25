using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using react.server.ApplicationContext;
using react.server.Models;
using server.data.Abstract;

namespace react.server.DataManagement.server.data.Abstract
{
    public interface IUserRepository: IRepository<Users>
    {
        Task<List<UserProfileModel>> UserProfilePost(string userId);
        Task<List<GetUserModel>> GetUsers();
        Task<List<GetUserModel>> GetUserProfile(string userId);
        Task<IActionResult> UserImage(IFormFile file, string userId);
        Task<IActionResult> ProfileEdit(ProfileEditModel model);
    }
}