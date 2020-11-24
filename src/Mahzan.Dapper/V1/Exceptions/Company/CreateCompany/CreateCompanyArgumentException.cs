using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Dapper.V1.Exceptions.Company.CreateCompany
{
    public class CreateCompanyArgumentException: ArgumentException
    {
        public CreateCompanyArgumentException(string message) : base(message)
        {

        }
    }
}
