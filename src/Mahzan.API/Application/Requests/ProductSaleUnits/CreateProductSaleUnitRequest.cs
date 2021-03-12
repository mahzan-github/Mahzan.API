using System;

namespace Mahzan.API.Application.Requests.ProductSaleUnits
{
    public class CreateProductSaleUnitRequest
    {
        public string  Abbreviation { get; set; }
        
        public string  Description { get; set; }
        
        public Guid  CompanyId { get; set; }  
    }
}