using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Dapper.V1.Exceptions.Company.CreateCompany
{
    public class CreateCompanyInvalidOperationException: InvalidOperationException
    {
        public CreateCompanyInvalidOperationException(string message) : base(message)
        {

        }
    }
}
