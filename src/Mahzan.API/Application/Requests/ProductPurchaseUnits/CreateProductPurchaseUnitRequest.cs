using System;
using System.ComponentModel.DataAnnotations;

namespace Mahzan.API.Application.Requests.ProductPurchaseUnits
{
    public class CreateProductPurchaseUnitRequest
    {
        [Required]
        [MaxLength(25)]
        public string Abbreviation { get; set; }
        [Required]
        [MaxLength(50)]
        public string Description { get; set; }
        [Required]
        public Guid CompanyId { get; set; }
    }
}