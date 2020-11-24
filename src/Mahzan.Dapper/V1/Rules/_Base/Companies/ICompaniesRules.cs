using Mahzan.Dapper.V1.Rules.Filters.Companies;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mahzan.Dapper.V1.Rules._Base.Companies
{
    public interface ICompaniesRules
    {
        Task<bool> CompanyExist(CompanyExistFilter filter);
    }
}
