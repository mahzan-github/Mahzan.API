using System;
using System.Collections.Generic;
using Mahzan.Models.Enums.ProductSalePrices;

namespace Mahzan.Business.V1.Commands.ProductSalePrices
{
    public class CalculateProductSalePricesCommand
    {
        public List<ProductTaxCommand> ProductTaxesCommand { get; set; }
        
        public List<ProductSalePriceCommand> ListProductSalePriceCommand { get; set; }
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
        
        public double  Price { get; set; }
        
        public double  Cost { get; set; }
    }
}