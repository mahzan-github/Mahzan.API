using System;
using System.ComponentModel.DataAnnotations;

namespace Mahzan.API.Application.Requests.ProductCategories
{
    public class CreateProductCategoryRequest
    {
        [MaxLength(25)]
        public string CodeCategory { get; set; }
        [Required]
        [MaxLength(50)]
        public string Description { get; set; }
        [Required]
        public Guid CompanyId { get; set; }
    }
}