using System;
using Mahzan.Persistance.V1.Dto.ProductCategories;

namespace Mahzan.Persistance.V1.ViewModel.ProductCategories
{
    public class CreateProductCategoryViewModel
    {
        public Guid ProductCategoryId { get; set; }

        public static CreateProductCategoryViewModel From(
            CreateProductCategoryDto createProductCategoryDto)
        {
            return new()
            {
                ProductCategoryId = createProductCategoryDto.ProductCategoryId
            };
        }
    }
}