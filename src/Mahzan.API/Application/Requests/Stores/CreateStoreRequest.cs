using System.ComponentModel.DataAnnotations;

namespace Mahzan.API.Application.Requests.Stores
{
    public class CreateStoreRequest
    {
        [MaxLength(50)]
        public string Code { get; set; }
     
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }
    }
}