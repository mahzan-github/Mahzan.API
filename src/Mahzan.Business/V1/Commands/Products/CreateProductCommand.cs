using System;
using System.Collections.Generic;
using Mahzan.Models.Enums.ProductSalePrices;

namespace Mahzan.Business.V1.Commands.Products
{
    public class CreateProductCommand
    {
        public ProductCommand ProductCommand { get; set; }
        
        public List<ProductTaxCommand> ProductTaxesCommand { get; set; }
        
        public List<ProductSalePriceCommand> ProductSalePricesCommand { get; set; }
    }
    
    public class ProductCommand
    {
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
    
    public class ProductTaxCommand
    {
        public Guid ProductTaxId { get; set; }
    }

    public class ProductSalePriceCommand
    {
        public PriceTypeEnum PriceTypeEnum { get; set; }

        public double  PricePurchase { get; set; }
        
        public bool  PriceNet { get; set; }
        
        public double  PricePurchaseUnitWitoutTaxes { get; set; }
        
        public double  PriceSaleUnitWitoutTaxes { get; set; }
        public double  Price { get; set; }
        
        public double  Cost { get; set; }
    }
}