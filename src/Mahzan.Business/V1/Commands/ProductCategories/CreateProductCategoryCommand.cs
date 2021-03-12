using System;

namespace Mahzan.Business.V1.Commands.ProductCategories
{
    public class CreateProductCategoryCommand
    {
        public string CodeCategory { get; set; }

        public string Description { get; set; }

        public Guid CompanyId { get; set; }
    }
}