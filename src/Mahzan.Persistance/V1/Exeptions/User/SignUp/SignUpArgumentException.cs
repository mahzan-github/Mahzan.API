using System;

namespace Mahzan.Persistance.V1.Exeptions.User.SignUp
{
    public class SignUpArgumentException:ArgumentException
    {
        public SignUpArgumentException(string message) : base(message)
        {
        }   
    }
}