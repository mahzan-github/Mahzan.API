using System;

namespace Mahzan.Persistance.V1.Exeptions.Company.CreateCompany
{
    public class CreateCompanyArgumentException:ArgumentException
    {
        public CreateCompanyArgumentException(string message) : base(message)
        {

        }
    }
}