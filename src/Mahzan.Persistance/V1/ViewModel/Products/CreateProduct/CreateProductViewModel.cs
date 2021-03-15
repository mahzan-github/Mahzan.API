using System;
using Mahzan.Persistance.V1.Dto.Products;

namespace Mahzan.Persistance.V1.ViewModel.Products.CreateProduct
{
    public class CreateProductViewModel
    {
        public Guid ProductId { get; set; }

        public static CreateProductViewModel From(CreateProductDto createProductDto)
        {
            return new CreateProductViewModel
            {
                ProductId = createProductDto.ProductId
            };
        }
    }
}