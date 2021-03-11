using System;

namespace Mahzan.Persistance.V1.Exeptions.User.ConfirmEmail
{
    public class ConfirmEmailInvalidOperationException:InvalidOperationException
    {
        public ConfirmEmailInvalidOperationException(string message) : base(message)
        {
        }
    }
}