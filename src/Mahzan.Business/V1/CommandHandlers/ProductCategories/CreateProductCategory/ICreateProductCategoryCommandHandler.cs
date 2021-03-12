using Mahzan.Business.V1.Commands.ProductCategories;
using Mahzan.Persistance.V1.ViewModel.ProductCategories;

namespace Mahzan.Business.V1.CommandHandlers.ProductCategories.CreateProductCategory
{
    public interface ICreateProductCategoryCommandHandler
        :ICommandHandler<CreateProductCategoryCommand, CreateProductCategoryViewModel>
    {
        
    }
}