using System;
using System.Collections.Generic;
using Mahzan.Models.Enums.ProductSalePrices;

namespace Mahzan.Persistance.V1.Dto.ProductSalePrices
{
    public class CreateSalePricesDto
    {
        public Guid ProductId { get; set; }
        public List<PriceDto> ListPriceDto { get; set; }
    }

    public class PriceDto
    {
        public PriceTypeEnum PriceTypeEnum { get; set; }

        public double  PricePurchase { get; set; }
        
        public double  PriceNet { get; set; }
        
        public double  PricePurchaseUnitWitoutTaxes { get; set; }
        
        public double  PriceSaleUnitWitoutTaxes { get; set; }
        
        public double?  Utility { get; set; }
        public double  Price { get; set; }
        public double  Cost { get; set; }
    }
}