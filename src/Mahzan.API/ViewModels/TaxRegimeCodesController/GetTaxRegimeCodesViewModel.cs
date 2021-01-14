using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mahzan.API.ViewModels.TaxRegimeCodesController
{
    public class GetTaxRegimeCodesViewModel
    {
        public Guid TaxRegimeCodeId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
