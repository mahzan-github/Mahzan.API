using System;

namespace Mahzan.Persistance.V1.Dto.ProductPurchaseUnits
{
    public record CreateProductPurchaseUnitDto
    {
        public Guid ProductPurchaseUnitId { get; init; }
        
        public string Abbreviation { get; set; }
        
        public string Description { get; set; }
        
        public Guid CompanyId { get; set; }
    }
}