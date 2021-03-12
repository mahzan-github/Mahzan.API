using System;

namespace Mahzan.Models.Entities
{
    public class ProductDepartments
    {
        public Guid ProductDepartmentId { get; set; }
        
        public string CodeDepartment { get; set; }

        public string Name { get; set; }

        public Guid CompanyId { get; set; }
    }
}