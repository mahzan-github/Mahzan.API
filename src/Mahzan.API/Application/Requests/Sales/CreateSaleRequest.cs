using System;
using System.Collections.Generic;

namespace Mahzan.API.Application.Requests.Sales
{
    public class CreateSaleRequest
    {
        public List<Product> Products { get; set; }
    }
    
    public class Product
    {
        public Guid ProductId { get; set; }
    }
}