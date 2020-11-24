using Mahzan.Dapper.V1.DTO.Company.UpdateCompany;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mahzan.Dapper.V1.Repositories.Company.UpdateCompany
{
    public interface IUpdateCompanyRepository
    {
        Task<Guid> Handle(UpdateCompanyDto updateCompanyDto);
    }
}
