using System;

namespace Mahzan.Models.Entities
{
    public class ProductTaxes
    {
        public Guid ProductTaxId { get; set; }
        
        public string Name { get; set; }
        
        public float percentage { get; set; }
        
        public bool PrintOnTicket { get; set; }
        
        public Guid CompanyId { get; set; }
    }
}