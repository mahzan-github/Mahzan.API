using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mahzan.API.Application.Requests.Products
{
    public class CreateProductRequest
    {
        public ProductRequest ProductRequest { get; set; }
        
        public List<ProductTaxRequest> ProductTaxesRequest { get; set; }
    }

    public class ProductRequest
    {
        [MaxLength(25)]
        public string KeyCode { get; set; }
        
        [MaxLength(25)]
        public string KeyAlternativeCode { get; set; }
        
        [MaxLength(100)]
        [Required]
        public string Description { get; set; }
        
        [Required]
        public Guid CompanyId { get; set; }
    }
    
    public class ProductTaxRequest
    {
        public Guid ProductTaxId { get; set; }
    }
}