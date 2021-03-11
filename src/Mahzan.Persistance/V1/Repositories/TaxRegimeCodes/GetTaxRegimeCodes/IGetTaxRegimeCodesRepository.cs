using Mahzan.Persistance.V1.Filters.TaxRegimeCodes;
using Mahzan.Persistance.V1.Repositories._Base;
using Mahzan.Persistance.V1.ViewModel.TaxRegimeCodes;

namespace Mahzan.Persistance.V1.Repositories.TaxRegimeCodes.GetTaxRegimeCodes
{
    public interface IGetTaxRegimeCodesRepository:IBaseFindRepository<GetTaxRegimeCodesViewModel,GetTaxRegimeCodesFilter>
    {
        
    }
}