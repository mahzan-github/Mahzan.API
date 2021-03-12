using System;

namespace Mahzan.Persistance.V1.Dto.ProductCategories
{
    public record CreateProductCategoryDto
    {
        public Guid ProductCategoryId { get; set; }
        public string CodeCategory { get; set; }

        public string Description { get; set; }

        public Guid CompanyId { get; set; }
    }
}