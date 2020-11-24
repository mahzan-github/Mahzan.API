using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mahzan.Business.V1.Exceptions.Company.CreateCompany
{
    public class CreateCompanyeventHandlerArgumentException: ArgumentException
    {
        public CreateCompanyeventHandlerArgumentException(string message) : base(message)
        {
        }
    }
}
