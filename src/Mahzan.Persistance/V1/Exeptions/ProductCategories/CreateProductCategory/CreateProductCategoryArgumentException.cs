using System;

namespace Mahzan.Persistance.V1.Exeptions.ProductCategories.CreateProductCategory
{
    public class CreateProductCategoryArgumentException:ArgumentException
    {
        public CreateProductCategoryArgumentException(string message) : base(message)
        {

        }
    }
}