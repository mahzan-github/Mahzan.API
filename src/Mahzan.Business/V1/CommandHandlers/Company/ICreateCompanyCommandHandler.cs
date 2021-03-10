using Mahzan.Business.V1.Commands.Company;
using Mahzan.Persistance.V1.ViewModel.Company;

namespace Mahzan.Business.V1.CommandHandlers.Company
{
    public interface ICreateCompanyCommandHandler: ICommandHandler<CreateCompanyCommand, CreateCompanyViewModel>
    {
        
    }
}