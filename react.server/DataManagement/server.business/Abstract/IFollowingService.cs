using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using react.server.Models;

namespace react.server.DataManagement.server.business.Abstract
{
    public interface IFollowingService
    {
        Task<IActionResult> FollowerAdd(FollowModel model);

        Task<IActionResult> FollowingControl(FollowModel model);
        Task<List<FollowingList>> TakipList(string userId);
        Task<List<FollowUserModel>> Takipciler(string userId);


    }
}