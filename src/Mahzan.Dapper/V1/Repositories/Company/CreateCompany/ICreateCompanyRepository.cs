using Mahzan.Dapper.V1.DTO.Company.CreateCompany;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mahzan.Dapper.V1.Repositories.Company.CreateCompany
{
    public interface ICreateCompanyRepository
    {
        Task<Guid> Handle(CreateCompanyDto createCompanyDto);
    }
}
