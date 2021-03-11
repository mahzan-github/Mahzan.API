using System;

namespace Mahzan.Persistance.V1.Exeptions.User.LogIn
{
    public class LoginArgumentException:ArgumentException
    {
        public LoginArgumentException(string message) : base(message)
        {
        }    
    }
}