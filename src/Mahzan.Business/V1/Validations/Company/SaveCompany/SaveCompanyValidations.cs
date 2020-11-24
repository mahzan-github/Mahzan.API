using Mahzan.Business.V1.Events.Company;
using Mahzan.Business.V1.Exceptions.Company.CreateCompany;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Mahzan.Business.V1.Validations.Company.SaveCompany
{
    public class SaveCompanyValidations : ISaveCompanyValidations
    {
        public async Task Handle(SaveCompanyEvent saveCompanyEvent)
        {
            //Valida RFC
            if (!await ValidateRFC(saveCompanyEvent.CompanyEvent.RFC))
            {
                throw new CreateCompanyeventHandlerArgumentException(
                    $"El RFC {saveCompanyEvent.CompanyEvent.RFC} no es válido."
                    );
            }
        }

        public async Task<bool> ValidateRFC(string rfc) 
        {
            return Regex.IsMatch(rfc,
                         @"/^([A-ZÑ&]{3,4}) ?(?:- ?)?(\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12]\d|3[01])) ?(?:- ?)?([A-Z\d]{2})([A\d])$/",
                         RegexOptions.IgnoreCase);
        }
    }
}
