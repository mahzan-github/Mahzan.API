using System;

namespace Mahzan.Models.Entities
{
    public class Products
    {
        public Guid ProductId { get; set; }
        
        public string KeyCode { get; set; }
        
        public string KeyAlternativeCode { get; set; }
        
        public string Description { get; set; }
        
        public Guid ProductCatagoryId { get; set; }
        
        public Guid ProductDepartmentId { get; set; }
        
        public Guid ProductPurchaseUnitId { get; set; }
        
        public Guid ProductSaleUnitId { get; set; }
        
        public double Factor { get; set; }
        
        public Guid CompanyId { get; set; }
    }
}