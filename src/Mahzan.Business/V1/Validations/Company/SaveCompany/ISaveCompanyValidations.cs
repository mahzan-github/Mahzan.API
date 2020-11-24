using Mahzan.Business.V1.Events.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mahzan.Business.V1.Validations.Company.SaveCompany
{
    public interface ISaveCompanyValidations
    {
        Task Handle(SaveCompanyEvent saveCompanyEvent);
    }
}
