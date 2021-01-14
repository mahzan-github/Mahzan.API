using Dapper;
using Mahzan.Dapper.V1.Filters.TaxRegimeCodes.GetTaxRegimeCodes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mahzan.Dapper.V1.Repositories.TaxRegimeCodes.GetTaxRegimeCodes
{
    public class GetTaxRegimeCodesRepository : DataConnection, IGetTaxRegimeCodesRepository
    {
        public GetTaxRegimeCodesRepository(IDbConnection dbConnection) 
            : base(dbConnection)
        {
        }

        public async Task<List<Models.Entities.TaxRegimeCodes>> GetTaxRegimeCodes(
            GetTaxRegimeCodesFilter filter)
        {

            DynamicParameters parameters = new DynamicParameters();

            string sql = @"select * from tax_regime_codes 
                           where 1=1 ";

            if (filter.Code!=null)
            {
                sql += "and code=@code ";
                parameters.Add("@code", filter.Code, DbType.String);
            }

            IEnumerable<Models.Entities.TaxRegimeCodes> taxRegimeCodes;
            taxRegimeCodes = await Connection
                .QueryAsync<Models.Entities.TaxRegimeCodes>(
                    sql,
                    parameters
                );

            return taxRegimeCodes.ToList();
        }
    }
}
