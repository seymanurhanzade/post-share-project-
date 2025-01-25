using Microsoft.EntityFrameworkCore;
using react.server.ApplicationContext;
using react.server.DataManagement.server.data.Abstract;
using react.server.Models;
using server.entitys;

namespace server.data.Concrate
{
    public class ConcrateAdminRepository : ConcrateRepository<Users, IdentityContext>, IAdminRepository
    {
        private readonly IdentityContext _context;

        public ConcrateAdminRepository(IdentityContext context) :base(context)
        {
            _context = context;
        }

        
    }

}