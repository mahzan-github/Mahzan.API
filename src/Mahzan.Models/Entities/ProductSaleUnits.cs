using System;

namespace Mahzan.Models.Entities
{
    public class ProductSaleUnits
    {
        public Guid ProductSaleUnitId { get; set; }

        public string Abbreviation { get; set; }
        
        public string Description { get; set; }
        
        public Guid CompanyId { get; set; }
    }
}