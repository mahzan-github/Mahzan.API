using Mahzan.Dapper.DTO.Users.LogIn;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mahzan.Dapper.Rules.Users.LogIn
{
    public interface ILoginRules
    {
        Task HandleRules(LoginDto loginDto);
    }
}
