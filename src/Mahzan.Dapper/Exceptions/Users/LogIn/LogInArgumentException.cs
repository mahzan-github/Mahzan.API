using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Dapper.Exceptions.Users.LogIn
{
    public class LoginArgumentException : ArgumentException
    {
        public LoginArgumentException(string message) : base(message)
        {
        }
    }
}
