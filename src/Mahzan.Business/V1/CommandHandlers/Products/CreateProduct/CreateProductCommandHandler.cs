using System;
using System.Threading.Tasks;
using Mahzan.Business.V1.Commands.Products;
using Mahzan.Persistance.V1.Dto.Products;
using Mahzan.Persistance.V1.Repositories.Products.CreateProduct;
using Mahzan.Persistance.V1.ViewModel.Products.CreateProduct;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Mahzan.Business.V1.CommandHandlers.Products.CreateProduct
{
    public class CreateProductCommandHandler
        :CommandHandlerBase<CreateProductCommand, CreateProductViewModel>, 
    ICreateProductCommandHandler
    {
        private readonly ICreateProductRepository _createProductRepository;
        
        public CreateProductCommandHandler(
            NpgsqlConnection connection,
            ILogger<CommandHandlerBase<CreateProductCommand, CreateProductViewModel>> logger, 
            ICreateProductRepository createProductRepository) 
            : base(connection, logger)
        {
            _createProductRepository = createProductRepository;
        }

        protected override async Task<CreateProductViewModel> HandleTransaction(
            CreateProductCommand command)
        {
            CreateProductDto createProductDto = await _createProductRepository
                .Insert(new CreateProductDto
                {
                    KeyCode = command.KeyCode,
                    KeyAlternativeCode = command.KeyAlternativeCode,
                    Description = command.Description
                });

            return CreateProductViewModel.From(createProductDto);
        }
    }
}