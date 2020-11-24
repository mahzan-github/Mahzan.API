using Mahzan.Dapper.V1.Rules.Filters.Companies;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using System.Collections.Generic;
using System.Linq;

namespace Mahzan.Dapper.V1.Rules._Base.Companies
{
    public class CompaniesRules : DataConnection,ICompaniesRules
    {
        public CompaniesRules(
            IDbConnection dbConnection) : base(dbConnection)
        {
        }


        public async Task<bool> CompanyExist(CompanyExistFilter filter)
        {
            bool result = false;

            DynamicParameters parameters = new DynamicParameters();

            string sql = @"
                select * from companies
                where 1=1
            ";

            if (filter.MemberId!=null)
            {
                sql += "and member_id = @member_id ";
                parameters.Add("@member_id", filter.MemberId, DbType.Guid);
            }

            if (filter.RFC != null)
            {
                sql += "and rfc = @rfc ";
                parameters.Add("@rfc", filter.RFC, DbType.String);
            }

            IEnumerable<Models.Entities.Companies> companies;
            companies = await Connection
                .QueryAsync<Models.Entities.Companies>(
                    sql,
                    parameters
                );

            if (companies.Any())
            {
                result = true;
            }

            return result;
        }
    }
}
