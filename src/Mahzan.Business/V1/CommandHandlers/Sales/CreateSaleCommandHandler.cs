using System.Threading.Tasks;
using Mahzan.Business.V1.Commands.Sales;
using Mahzan.Persistance.V1.Dto.Sales;
using Mahzan.Persistance.V1.Repositories.Sales;
using Mahzan.Persistance.V1.ViewModel.Sales;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Mahzan.Business.V1.CommandHandlers.Sales
{
    public class CreateSaleCommandHandler
        : CommandHandlerBase<CreateSaleCommand, CreateSaleViewModel>, ICreateSaleCommandHandler
    {
        private readonly ICreateSaleRepository _createSaleRepository;

        public CreateSaleCommandHandler(
            ICreateSaleRepository createSaleRepository)
        {
            _createSaleRepository = createSaleRepository;
        }

        protected override async Task<CreateSaleViewModel> HandleTransaction(CreateSaleCommand command)
        {

            await _createSaleRepository.Insert(new CreateSaleDto
            {
                
            });

            return new CreateSaleViewModel()
            {
                
            };
        }
    }
}