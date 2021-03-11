using System;

namespace Mahzan.Persistance.V1.Exeptions.User.LogIn
{
    public class LoginInvalidOperationException:InvalidOperationException
    {
        public LoginInvalidOperationException(string message) : base(message)
        {
        }
    }
}