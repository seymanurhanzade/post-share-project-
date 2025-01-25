using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using react.server.DataManagement.server.entitys;
using react.server.Models;
using server.data.Abstract;

namespace react.server.DataManagement.server.data.Abstract
{
    public interface IFollowingRepository: IRepository<FollowerTable>
    {
        Task<IActionResult> FollowerAdd(FollowModel model);
        Task<IActionResult> FollowingControl(FollowModel model);
        Task<List<FollowingList>> TakipList(string userId);
        Task<List<FollowUserModel>> Takipciler(string userId);
    }
}