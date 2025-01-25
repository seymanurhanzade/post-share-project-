using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using react.server.Models;

namespace server.business.Abstract
{
    public interface IAuthService
    {
        Task Register(UserRegisterDTO model);
        Task<ILoginToken> Login(UserLoginDTO model);
    }
}