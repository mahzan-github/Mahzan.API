using System;
using System.ComponentModel.DataAnnotations;

namespace Mahzan.API.Application.Requests.ProductDepartmets
{
    public class CreateProductDepartmentRequest
    {
        [MaxLength(25)]
        public string CodeDepartment { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public Guid CompanyId { get; set; }
    }
}