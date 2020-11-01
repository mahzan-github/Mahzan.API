using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mahzan.API.Exceptions
{
    public class ServiceInvalidOperationException : InvalidOperationException
    {
        public ServiceInvalidOperationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ServiceInvalidOperationException(Exception innerException) : this(innerException.Message, innerException)
        {
        }


    }
}
