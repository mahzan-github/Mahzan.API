using Mahzan.Dapper.DTO.Users.SignUp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mahzan.Dapper.Rules.Users.SignUp
{
    public interface ISignUpRules
    {
        Task HandleRules(SignUpDto signUpDto);
    }
}
