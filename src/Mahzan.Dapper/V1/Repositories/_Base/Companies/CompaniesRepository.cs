using Mahzan.Dapper.V1.Filters._Base.Companies;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Linq;

namespace Mahzan.Dapper.V1.Repositories._Base.Companies
{
    public class CompaniesRepository : DataConnection,ICompaniesRepository
    {
        public CompaniesRepository(
            IDbConnection dbConnection) : base(dbConnection)
        {
        }

        public async Task<List<Models.Entities.Companies>> GetCompaniesByFilterAsync(GetCompaniesFilter filter)
        {
            List<Models.Entities.Companies> result = null;

            DynamicParameters parameters = new DynamicParameters();

            string sql = @"
                select * 
                from companies
                where 1=1
            ";

            if (filter.MemberId!=null)
            {
                sql += "and member_id= @member_id ";
                parameters.Add("@member_id", filter.MemberId, DbType.Guid);
            }

            if (filter.CompanyId != null)
            {
                sql += "and company_id= @company_id ";
                parameters.Add("@company_id", filter.CompanyId, DbType.Guid);
            }

            IEnumerable<Models.Entities.Companies> companies;
            companies = await Connection
                .QueryAsync<Models.Entities.Companies>(
                    sql,
                    parameters
                );

            if (companies.Any())
            {
                result = companies.ToList();
            }

            return result;
        }
    }
}
