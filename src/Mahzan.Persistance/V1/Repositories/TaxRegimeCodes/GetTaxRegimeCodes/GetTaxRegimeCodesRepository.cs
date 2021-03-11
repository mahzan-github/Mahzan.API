using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Mahzan.Persistance.V1.Filters.TaxRegimeCodes;
using Mahzan.Persistance.V1.Repositories._Base;
using Mahzan.Persistance.V1.ViewModel.TaxRegimeCodes;
using Npgsql;

namespace Mahzan.Persistance.V1.Repositories.TaxRegimeCodes.GetTaxRegimeCodes
{
    public class GetTaxRegimeCodesRepository:
    BaseFindRepository<GetTaxRegimeCodesViewModel,GetTaxRegimeCodesFilter>,
    IGetTaxRegimeCodesRepository
    {
        public GetTaxRegimeCodesRepository(NpgsqlConnection connection) : base(connection,
            model => model.Code.ToString())
        {
        }

        protected override async Task<IReadOnlyList<GetTaxRegimeCodesViewModel>> FindInternal(
            GetTaxRegimeCodesFilter filter, 
            PagingOptions pagingOptions)
        {
            DynamicParameters parameters = new();
            string sql = @"
                select * from tax_regime_codes
                where 1 = 1
            ";

            if (filter.Code!=null)
            {
                sql += "and code=@code ";
                parameters.Add("@code", filter.Code, DbType.String);
            }

            IEnumerable<GetTaxRegimeCodesViewModel> getTaxRegimeCodesViewModel = await Connection
                .QueryAsync<GetTaxRegimeCodesViewModel>(
                    sql,
                    parameters
                );
            
            return getTaxRegimeCodesViewModel.ToImmutableList();
        }
        

    }
}