using System;

namespace Mahzan.Models.Entities
{
    public class ProductPurchaseUnits
    {
        public Guid ProductPurchaseUnitId { get; set; }

        public string Abbreviation { get; set; }
        
        public string Description { get; set; }
        
        public Guid CompanyId { get; set; }
    }
}