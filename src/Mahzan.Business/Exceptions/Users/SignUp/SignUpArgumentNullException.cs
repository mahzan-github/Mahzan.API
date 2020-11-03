using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Business.Exceptions.Users.SignUp
{
    public class SignUpArgumentNullException: ArgumentNullException
    {
        public SignUpArgumentNullException(string message) : base(message)
        {
        }
    }
}
