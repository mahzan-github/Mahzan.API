using System.Collections.Generic;

namespace Mahzan.Persistance.V1.ViewModel.TaxRegimeCodes
{
    public record GetTaxRegimeCodesViewModel
    {
        public string Code { get; init; }
        
        public string Description { get; init; }
    }
}