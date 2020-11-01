using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Dapper.Exceptions.Users.SignUp
{
    public class SignUpArgumentException : ArgumentException
    {
        public SignUpArgumentException(string message) : base(message)
        {
        }
    }
}
