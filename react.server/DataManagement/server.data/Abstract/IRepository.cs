using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.data.Abstract
{
    public interface IRepository<T> where T : class
    {
        T GetById(int id);
        List<T> GetAll();
        void Create (T entity);
        void Update (T entity);
        void Delete (T entity);
    }
}