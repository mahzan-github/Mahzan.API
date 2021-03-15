using System.ComponentModel.DataAnnotations;

namespace Mahzan.API.Application.Requests.Products
{
    public class CreateProductRequest
    {
        [MaxLength(25)]
        public string KeyCode { get; set; }
        
        [MaxLength(25)]
        public string KeyAlternativeCode { get; set; }
        
        [MaxLength(100)]
        [Required]
        public string Description { get; set; }
    }
}