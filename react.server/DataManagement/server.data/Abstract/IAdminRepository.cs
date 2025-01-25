using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using react.server.ApplicationContext;
using react.server.Models;
using server.data.Abstract;
using server.entitys;

namespace react.server.DataManagement.server.data.Abstract
{
    public interface IAdminRepository: IRepository<Users>
    {
        
    }
}