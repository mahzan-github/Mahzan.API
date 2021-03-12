using System;

namespace Mahzan.Persistance.V1.Dto.ProductDepartments
{
    public record CreateProductDepartmentDto
    {
        public Guid ProductDepartmentId { get; set; }
        
        public string CodeDepartment { get; set; }

        public string Name { get; set; }

        public Guid CompanyId { get; set; }   
    }
}