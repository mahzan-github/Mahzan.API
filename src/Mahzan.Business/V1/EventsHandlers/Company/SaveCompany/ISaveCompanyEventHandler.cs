using Mahzan.Business.V1.Events.Company;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mahzan.Business.V1.EventsHandlers.Company.SaveCompany
{
    public interface ISaveCompanyEventHandler
    {
        Task<Guid> Handler(SaveCompanyEvent createCompanyEvent);
    }
}
