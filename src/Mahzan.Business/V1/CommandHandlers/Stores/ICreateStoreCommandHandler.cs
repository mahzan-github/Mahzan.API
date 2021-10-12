using Mahzan.Business.V1.Commands.Stores;
using Mahzan.Persistance.V1.ViewModel.Stores;

namespace Mahzan.Business.V1.CommandHandlers.Stores
{
    public interface ICreateStoreCommandHandler
        :ICommandHandler<CreateStoreCommand, CreateStoreViewModel>
    {
        
    }
}