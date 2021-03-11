using System;

namespace Mahzan.Persistance.V1.Exeptions.User.ConfirmEmail
{
    public class ConfirmEmailArgumentException:ArgumentException
    {
        public ConfirmEmailArgumentException(string message) : base(message)
        {
        } 
    }
}