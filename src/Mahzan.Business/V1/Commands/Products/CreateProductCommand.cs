using System;
using System.Collections.Generic;

namespace Mahzan.Business.V1.Commands.Products
{
    public class CreateProductCommand
    {
        public ProductCommand ProductCommand { get; set; }
        
        public List<ProductTaxCommand> ProductTaxesCommand { get; set; }
    }
    
    public class ProductCommand
    {
        public string KeyCode { get; set; }
        
        public string KeyAlternativeCode { get; set; }
        
        public string Description { get; set; }

        public Guid CompanyId { get; set; }
    }
    
    public class ProductTaxCommand
    {
        public Guid ProductTaxId { get; set; }
    }
}