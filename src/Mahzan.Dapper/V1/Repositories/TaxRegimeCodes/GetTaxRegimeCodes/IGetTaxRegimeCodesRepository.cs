using Mahzan.Dapper.V1.Filters.TaxRegimeCodes.GetTaxRegimeCodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mahzan.Dapper.V1.Repositories.TaxRegimeCodes.GetTaxRegimeCodes
{
    public interface IGetTaxRegimeCodesRepository
    {
        Task<List<Models.Entities.TaxRegimeCodes>> GetTaxRegimeCodes(GetTaxRegimeCodesFilter filter);
    }
}
