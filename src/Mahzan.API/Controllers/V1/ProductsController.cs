using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Mahzan.API.Application.Requests.Products;
using Mahzan.API.Exceptions;
using Mahzan.Business.V1.CommandHandlers.Products.CreateProduct;
using Mahzan.Business.V1.Commands.Products;
using Mahzan.Persistance.V1.Dto.Products;
using Mahzan.Persistance.V1.ViewModel.Products.CreateProduct;
using Microsoft.AspNetCore.Mvc;

namespace Mahzan.API.Controllers.V1
{
    [ApiVersion("1")]
    [Route("v{version:apiVersion}")]
    [ApiController]
    public class ProductsController : Controller
    {
        private ICreateProductCommandHandler _createProductCommandHandler;

        public ProductsController(
            ICreateProductCommandHandler createProductCommandHandler)
        {
            _createProductCommandHandler = createProductCommandHandler;
        }


        [HttpPost("product:create")]
        [ProducesResponseType(typeof(CreateProductViewModel), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> CreateProductPurchaseUnit(
            CreateProductRequest request)
        {
            CreateProductViewModel createProductViewModel;
            CreateProductCommand createProductCommand = new CreateProductCommand();
            try
            {
                // Product (Required)
                createProductCommand.ProductCommand = new ProductCommand
                {
                    KeyCode = request.ProductRequest.KeyCode,
                    KeyAlternativeCode = request.ProductRequest.KeyAlternativeCode,
                    Description = request.ProductRequest.Description,
                    ProductCatagoryId = request.ProductRequest.ProductCatagoryId,
                    ProductDepartmentId = request.ProductRequest.ProductDepartmentId,
                    ProductPurchaseUnitId = request.ProductRequest.ProductPurchaseUnitId,
                    ProductSaleUnitId = request.ProductRequest.ProductSaleUnitId,
                    Factor = request.ProductRequest.Factor,
                    CompanyId = request.ProductRequest.CompanyId
                };
                
                // Taxes (Optional)
                if (request.ProductTaxesRequest!=null)
                {
                    createProductCommand.ProductTaxesCommand = request
                        .ProductTaxesRequest
                        .Select(p => new ProductTaxCommand
                        {
                            ProductTaxId = p.ProductTaxId
                        })
                        .ToList();
                }
                
                // Product Price & Cost (Required)
                createProductCommand.ProductSalePricesCommand = request
                    .ProductSalePriceRequest
                    .Select(p => new ProductSalePriceCommand
                    {
                        PriceTypeEnum = p.PriceTypeEnum,
                        Price = p.Price,
                        Cost = p.Cost
                    })
                    .ToList();
                
                createProductViewModel  = await _createProductCommandHandler
                    .Handle(createProductCommand);
            }
            catch (ArgumentException e)
            {
                throw new ServiceArgumentException(e);
            }

            return Ok(createProductViewModel);
        }
    }
}