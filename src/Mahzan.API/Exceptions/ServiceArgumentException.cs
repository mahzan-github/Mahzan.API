using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mahzan.API.Exceptions
{
    public class ServiceArgumentException : ArgumentException
    {
        public ServiceArgumentException(string message, Exception exception)
            : base(message, exception)
        { }

        public ServiceArgumentException(Exception exception)
            : this(exception.Message, exception)
        { }
    }
}
