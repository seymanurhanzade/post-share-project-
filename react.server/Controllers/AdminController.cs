using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using react.server.ApplicationContext;
using react.server.DataManagement.server.data.Abstract;
using react.server.Models;
using server.business.Abstract;
using server.entitys;

namespace react.server.Controllers
{
    // [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController:ControllerBase
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService){
            _adminService = adminService;
           
        }

        [HttpGet("GetUsers")]
        public  List<Users> GetUsers(){
            return _adminService.GetUsers();
        }

        [HttpGet("GetPosts")]
        public List<InstantShare> GetPosts(){
            return  _adminService.GetPosts();
        }
        
        [HttpDelete("Delete")]
        public IActionResult PostDelete([FromQuery] int id){

        _adminService.DeleteAndAdd(id);
        return Ok();
        }

    }
}