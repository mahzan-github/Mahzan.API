using Mahzan.Dapper.DTO.Users.SignUp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mahzan.Dapper.Repositories.Users.SignUp
{
    public interface ISignUpRepository
    {
        Task<Models.Entities.Users> HandleRepository(SignUpDto signUpDto);
    }
}
