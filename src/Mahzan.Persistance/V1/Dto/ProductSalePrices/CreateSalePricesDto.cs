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
        public decimal  Price { get; set; }
        public decimal  Cost { get; set; }
    }
}