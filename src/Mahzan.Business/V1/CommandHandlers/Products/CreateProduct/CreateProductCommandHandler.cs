using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mahzan.Business.V1.Commands.Products;
using Mahzan.Persistance.V1.Dto.Products;
using Mahzan.Persistance.V1.Dto.ProductSalePrices;
using Mahzan.Persistance.V1.Dto.ProductSaleTaxes;
using Mahzan.Persistance.V1.Repositories.Products.CreateProduct;
using Mahzan.Persistance.V1.Repositories.ProductSalePrices.CreateProductSalePrices;
using Mahzan.Persistance.V1.Repositories.ProductSaleTaxes.CreateProductSaleTax;
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

        private readonly ICreateProductSaleTax _createProductSaleTax;

        private readonly ICreateProductSalePricesRepository _createProductSalePricesRepository;
        
        public CreateProductCommandHandler(
            NpgsqlConnection connection,
            ILogger<CommandHandlerBase<CreateProductCommand, CreateProductViewModel>> logger, 
            ICreateProductRepository createProductRepository, 
            ICreateProductSaleTax createProductSaleTax, 
            ICreateProductSalePricesRepository createProductSalePricesRepository) 
            : base(connection, logger)
        {
            _createProductRepository = createProductRepository;
            _createProductSaleTax = createProductSaleTax;
            _createProductSalePricesRepository = createProductSalePricesRepository;
        }

        protected override async Task<CreateProductViewModel> HandleTransaction(
            CreateProductCommand command)
        {
            //Producto
            CreateProductDto createProductDto = await InsertInProducts(command);

            //Impuestos del Producto
            if (command.ProductTaxesCommand.Any())
            {
                await InsertInProductSaleTaxes(
                    createProductDto.ProductId,
                    command.ProductTaxesCommand);
            }

            //Precios del Producto
            if (command.ProductSalePricesCommand.Any())
            {
                await InsertInProductSalePrices(
                    createProductDto.ProductId,
                    command.ProductSalePricesCommand
                    );
            }
            
            return CreateProductViewModel.From(createProductDto);
        }

        #region :: Create Product Handler Steps ::

        private async Task<CreateProductDto> InsertInProducts(CreateProductCommand command)
        {
            CreateProductDto createProductDto = await _createProductRepository
                .Insert(new CreateProductDto
                {
                    KeyCode = command.ProductCommand.KeyCode,
                    KeyAlternativeCode = command.ProductCommand.KeyAlternativeCode,
                    Description = command.ProductCommand.Description,
                    CompanyId = command.ProductCommand.CompanyId
                });

            return createProductDto;
        }
        
        private async Task InsertInProductSaleTaxes(
            Guid productId,
            List<ProductTaxCommand> listProductTaxCommand)
        {
            CreateProductSaleTaxDto createProductSaleTaxDto = await _createProductSaleTax
                .Insert(new CreateProductSaleTaxDto
                {
                    ListProductTaxDto = listProductTaxCommand
                        .Select(l => new ProductTaxDto
                        {
                            ProductTaxId = l.ProductTaxId,
                            ProductId = productId
                        })
                        .ToList()
                });
            
        }

        private async Task InsertInProductSalePrices(
            Guid productId,
            List<ProductSalePriceCommand> listProductSalePriceCommand)
        {
            await _createProductSalePricesRepository
                .Insert(new CreateSalePricesDto
                {
                    ListPriceDto = listProductSalePriceCommand
                        .Select(l => new PriceDto
                        {
                            PriceTypeEnum = l.PriceTypeEnum,
                            Price = l.Price,
                            Cost = l.Cost
                        })
                        .ToList(),
                    ProductId = productId
                });
        }

        #endregion
    }
}