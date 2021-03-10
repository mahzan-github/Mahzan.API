using System.Threading.Tasks;
using Mahzan.Business.V1.Commands.Company;
using Mahzan.Persistance.V1.ViewModel.Company;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Mahzan.Business.V1.CommandHandlers.Company
{
    public class CreateCompanyCommandHandler
    :CommandHandlerBase<CreateCompanyCommandHandler,CreateCompanyViewModel>, ICreateCompanyCommandHandler
    {
        public CreateCompanyCommandHandler(
            NpgsqlConnection connection, 
            ILogger<CommandHandlerBase<CreateCompanyCommandHandler, CreateCompanyViewModel>> logger) 
            : base(connection, logger)
        {
        }

        protected override Task<CreateCompanyViewModel> HandleTransaction(CreateCompanyCommandHandler command)
        {
            throw new System.NotImplementedException();
        }

        public Task<CreateCompanyViewModel> Handle(CreateCompanyCommand command)
        {
            throw new System.NotImplementedException();
        }
    }
}