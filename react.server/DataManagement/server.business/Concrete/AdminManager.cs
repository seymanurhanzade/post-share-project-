
using System;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using react.server.ApplicationContext;
using react.server.DataManagement.server.data.Abstract;
using react.server.Models;
using server.business.Abstract;
using server.data.Concrate;
using server.entitys;

namespace server.business.Concrete
{
    public class AdminManager : IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IInstantShareRepository _instantrepository;
        private readonly IdentityContext _context;
        public AdminManager(IAdminRepository adminRepository,IInstantShareRepository instantShareRepository,IdentityContext context){
            _adminRepository = adminRepository;
            _instantrepository = instantShareRepository;
            _context = context;
        }
        public InstantShare GetById(int id)
        {
            return _instantrepository.GetById(id);
        }

        public List<InstantShare> GetPosts()
        {
            return _instantrepository.GetAll();
        }

        public List<Users> GetUsers()
        {
            return _adminRepository.GetAll();
        }

        public void Update(InstantShare entity)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAndAdd(int id)
        {
            var data = GetById(id);
            var dltData = new DeletePost{
                Share= data.Share,
                Time= DateTime.Now,
                UserId = data.UserId,
            };
            await _context.DeletePost.AddAsync(dltData);
            
            _instantrepository.Delete(data);
            _context.SaveChangesAsync();
     
        }
    }
}