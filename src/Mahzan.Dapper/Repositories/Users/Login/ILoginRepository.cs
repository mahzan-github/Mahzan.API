using Mahzan.Dapper.DTO.Users.LogIn;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mahzan.Dapper.Repositories.Users.Login
{
    public interface ILoginRepository
    {
        Task<Models.Entities.Users> HandleRepository(LoginDto loginDto);
    }
}
