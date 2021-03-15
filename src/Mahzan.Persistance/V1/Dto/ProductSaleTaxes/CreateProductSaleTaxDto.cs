using System;
using System.Collections.Generic;

namespace Mahzan.Persistance.V1.Dto.ProductSaleTaxes
{
    public class CreateProductSaleTaxDto
    {
        public List<ProductTaxDto> ListProductTaxDto { get; set; }
    }
    
    public class ProductTaxDto
    {
        public Guid ProductSaleTaxId { get; set; }
        public Guid ProductTaxId { get; set; }
        public Guid ProductId { get; set; }
    }
}