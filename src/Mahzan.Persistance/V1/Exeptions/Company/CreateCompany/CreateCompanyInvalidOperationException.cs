using System;

namespace Mahzan.Persistance.V1.Exeptions.Company.CreateCompany
{
    public class CreateCompanyInvalidOperationException:InvalidOperationException
    {
        public CreateCompanyInvalidOperationException(string message) : base(message)
        {

        }
    }
}