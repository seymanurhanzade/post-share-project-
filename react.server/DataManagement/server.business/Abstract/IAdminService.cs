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
    public interface IAdminService
    {
        List<Users> GetUsers();
        List<InstantShare> GetPosts();
        void Update(InstantShare entity);
        InstantShare GetById(int id);
        Task DeleteAndAdd(int id);
    }
}