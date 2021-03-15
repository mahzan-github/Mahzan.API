using System;

namespace Mahzan.API.Application.Requests.ProductTaxes
{
    public class CreateProductTaxRequest
    {
        public string Name { get; set; }
        
        public float percentage { get; set; }
        
        public bool PrintOnTicket { get; set; }
        
        public Guid CompanyId { get; set; }
    }
}