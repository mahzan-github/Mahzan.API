using Mahzan.Business.V1.Commands.Sales;
using Mahzan.Persistance.V1.ViewModel.Sales;

namespace Mahzan.Business.V1.CommandHandlers.Sales
{
    public interface ICreateSaleCommandHandler
        :ICommandHandler<CreateSaleCommand, CreateSaleViewModel>
    {
        
    }
}