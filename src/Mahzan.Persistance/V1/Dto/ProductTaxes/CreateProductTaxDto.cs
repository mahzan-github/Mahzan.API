using System;

namespace Mahzan.Persistance.V1.Dto.ProductTaxes
{
    public record CreateProductTaxDto
    {
        public Guid ProductTaxId { get; init; }
        
        public string Name { get; set; }
        
        public float Percentage { get; set; }
        
        public bool PrintOnTicket { get; set; }
        
        public Guid CompanyId { get; set; }
    }
}