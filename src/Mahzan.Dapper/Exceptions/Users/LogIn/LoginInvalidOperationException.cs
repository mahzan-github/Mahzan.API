using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Dapper.Exceptions.Users.LogIn
{
    public class LoginInvalidOperationException : InvalidOperationException
    {
        public LoginInvalidOperationException(string message) : base(message)
        {
        }
    }
}
