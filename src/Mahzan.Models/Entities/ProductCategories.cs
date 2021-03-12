using System;

namespace Mahzan.Models.Entities
{
    public class ProductCategories
    {
        public Guid ProductCatagoryId { get; set; }
        
        public string CodeCategory { get; set; }

        public string Description { get; set; }

        public Guid CompanyId { get; set; }
    }
}