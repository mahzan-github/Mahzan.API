using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Mahzan.Models.Enums.ProductSalePrices;

namespace Mahzan.API.Application.Requests.Products
{
    public class CreateProductRequest
    {
        [Required]
        public ProductRequest ProductRequest { get; set; }
        public List<ProductTaxRequest> ProductTaxesRequest { get; set; }
        [Required]
        public List<ProductSalePriceRequest> ProductSalePriceRequest { get; set; }
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
        
        public Guid? ProductCatagoryId { get; set; }
        
        public Guid? ProductDepartmentId { get; set; }
        
        public Guid? ProductPurchaseUnitId { get; set; }
        
        public Guid? ProductSaleUnitId { get; set; }
        
        public double? Factor { get; set; }
        [Required]
        public Guid CompanyId { get; set; }
    }
    
    public class ProductTaxRequest
    {
        public Guid ProductTaxId { get; set; }
    }
    
    public class ProductSalePriceRequest
    {
        public PriceTypeEnum PriceTypeEnum { get; set; }
        public decimal  Price { get; set; }
        public decimal  Cost { get; set; }
    }
}