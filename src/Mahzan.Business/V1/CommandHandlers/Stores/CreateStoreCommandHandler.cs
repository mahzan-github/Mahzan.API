using System.Threading.Tasks;
using Mahzan.Business.V1.Commands.Stores;
using Mahzan.Persistance.V1.Dto.Stores;
using Mahzan.Persistance.V1.Repositories.Stores;
using Mahzan.Persistance.V1.ViewModel.Stores;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Mahzan.Business.V1.CommandHandlers.Stores
{
    public class CreateStoreCommandHandler
        : CommandHandlerBase<CreateStoreCommand, CreateStoreViewModel>, ICreateStoreCommandHandler
    {
        private readonly ICreateStoreRepository _createStoreRepository;

        public CreateStoreCommandHandler(
            NpgsqlConnection connection, 
            ILogger<CommandHandlerBase<CreateStoreCommand, CreateStoreViewModel>> logger,
            ICreateStoreRepository createStoreRepository):base(connection, logger)
        {
            _createStoreRepository = createStoreRepository;
        }

        protected override async Task<CreateStoreViewModel> HandleTransaction(CreateStoreCommand command)
        {
            await _createStoreRepository.Insert(new CreateStoreDto
            {
                Name = command.Name,
                Code = command.Code,
                MemberId = command.MemberId
            });

            return new CreateStoreViewModel
            {
                
            };
        }
    }
}