using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medium_Clone_WebAPI.DataAccess.Interfaces
{
    public interface IRepository<T>
    {
        IEnumerable<CurentUser> GetAll();
        CurentUser GetById(int id);
        void Add(CurentUser user);
    }
}
