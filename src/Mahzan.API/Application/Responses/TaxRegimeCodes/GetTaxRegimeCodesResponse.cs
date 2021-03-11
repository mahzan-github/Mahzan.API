using System.Collections.Generic;

namespace Mahzan.API.Application.Responses.TaxRegimeCodes
{
    public class GetTaxRegimeCodesResponse
    {
        public List<TaxRegimeCodesResponse> ListTaxRegimeCodesResponse { get; set; }
    }

    public class TaxRegimeCodesResponse
    {
        public string Code { get; init; }
        
        public string Description { get; init; }
    }
}