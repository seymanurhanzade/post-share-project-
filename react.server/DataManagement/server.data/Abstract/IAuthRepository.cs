using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using react.server.ApplicationContext;
using react.server.Models;

namespace server.data.Abstract
{
    public interface IAuthRepository: IRepository<Users>
    {
        Task Register(UserRegisterDTO model);
        Task<string> Login(UserLoginDTO model);
    }
}