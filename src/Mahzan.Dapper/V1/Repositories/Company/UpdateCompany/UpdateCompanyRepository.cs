using Mahzan.Dapper.V1.DTO.Company.UpdateCompany;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Mahzan.Dapper.V1.Repositories.Company.UpdateCompany
{
    public class UpdateCompanyRepository : DataConnection,IUpdateCompanyRepository
    {
        public UpdateCompanyRepository(
            IDbConnection dbConnection) : base(dbConnection)
        {
        }

        public async Task<Guid> Handle(UpdateCompanyDto updateCompanyDto)
        {
            throw new NotImplementedException();
        }
    }
}
