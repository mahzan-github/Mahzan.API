using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mahzan.API.Exceptions
{
    public class ServiceKeyNotFoundException : KeyNotFoundException
    {
        public ServiceKeyNotFoundException(string message)
            : base(message)
        { }

        public ServiceKeyNotFoundException(string message, Exception exception)
            : base(message, exception)
        { }

        public ServiceKeyNotFoundException(Exception exception)
            : this(exception.Message, exception)
        { }


    }
}
