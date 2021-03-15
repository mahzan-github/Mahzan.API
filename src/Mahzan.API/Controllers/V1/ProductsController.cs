using System;
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
            try
            {
                createProductViewModel  = await _createProductCommandHandler
                    .Handle(new CreateProductCommand
                    {
                        KeyCode = request.KeyCode,
                        KeyAlternativeCode = request.KeyAlternativeCode,
                        Description = request.Description
                    });
            }
            catch (ArgumentException e)
            {
                throw new ServiceArgumentException(e);
            }

            return Ok(createProductViewModel);
        }
    }
}