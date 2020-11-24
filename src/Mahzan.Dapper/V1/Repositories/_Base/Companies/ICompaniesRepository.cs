using Mahzan.Dapper.V1.Filters._Base.Companies;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mahzan.Dapper.V1.Repositories._Base.Companies
{
    public interface ICompaniesRepository
    {
        Task<List<Models.Entities.Companies>> GetCompaniesByFilterAsync(GetCompaniesFilter filter);
    }
}
