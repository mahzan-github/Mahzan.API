

using Mahzan.Business.V1.Commands.Products;
using Mahzan.Persistance.V1.ViewModel.Products.CreateProduct;

namespace Mahzan.Business.V1.CommandHandlers.Products.CreateProduct
{
    public interface ICreateProductCommandHandler
        :ICommandHandler<CreateProductCommand, CreateProductViewModel>

    {
        
    }
}