using System;

namespace Mahzan.Persistance.V1.Dto.ProductSaleUnits
{
    public record CreateProductSaleUnitDto
    {
        public Guid ProductSaleUnitId { get; set; }
        
        public string  Abbreviation { get; set; }
        
        public string  Description { get; set; }
        
        public Guid  CompanyId { get; set; }
    }
}