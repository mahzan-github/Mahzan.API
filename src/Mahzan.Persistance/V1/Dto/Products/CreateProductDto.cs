using System;

namespace Mahzan.Persistance.V1.Dto.Products
{
    public record CreateProductDto
    {
        public Guid ProductId { get; init; }

        public string KeyCode { get; set; }
        
        public string KeyAlternativeCode { get; set; }
        
        public string Description { get; set; }
        
        public Guid? ProductCatagoryId { get; set; }
        
        public Guid? ProductDepartmentId { get; set; }
        
        public Guid? ProductPurchaseUnitId { get; set; }
        
        public Guid? ProductSaleUnitId { get; set; }
        
        public double? Factor { get; set; }
        
        public Guid CompanyId { get; set; }
    }
}