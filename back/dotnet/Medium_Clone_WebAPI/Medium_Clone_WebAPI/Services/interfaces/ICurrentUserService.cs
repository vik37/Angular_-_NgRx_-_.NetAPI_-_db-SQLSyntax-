using Medium_Clone_WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medium_Clone_WebAPI.Services.interfaces
{
    public interface ICurrentUserService
    {
        IEnumerable<CurentUserDto> GetAll();
        CurentUserDto GetById(int id);
        CurentUserDto RegisterNewUSer(RegisterDto register);
    }
}
