using System;
using System.Net;
using System.Threading.Tasks;
using Mahzan.API.Application.Requests.ProductCategories;
using Mahzan.API.Exceptions;
using Mahzan.Business.V1.CommandHandlers.ProductCategories.CreateProductCategory;
using Mahzan.Business.V1.Commands.ProductCategories;
using Mahzan.Persistance.V1.ViewModel.ProductCategories;
using Microsoft.AspNetCore.Mvc;

namespace Mahzan.API.Controllers.V1
{
    [ApiVersion("1")]
    [Route("v{version:apiVersion}")]
    [ApiController]
    public class ProductCategoriesController : Controller
    {
        private readonly ICreateProductCategoryCommandHandler _createProductCategoryCommandHandler;

        public ProductCategoriesController(
            ICreateProductCategoryCommandHandler createProductCategoryCommandHandler)
        {
            _createProductCategoryCommandHandler = createProductCategoryCommandHandler;
        }


        [HttpPost("product-category:create")]
        [ProducesResponseType(typeof(CreateProductCategoryViewModel), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> Create(CreateProductCategoryRequest request)
        {
            CreateProductCategoryViewModel createProductCategoryViewModel = null;
            try
            {
                createProductCategoryViewModel = await _createProductCategoryCommandHandler
                    .Handle(new CreateProductCategoryCommand
                    {
                        CodeCategory = request.CodeCategory,
                        Description = request.Description,
                        CompanyId = request.CompanyId
                    });
            }
            catch (ArgumentException e)
            {
                throw new ServiceArgumentException(e);
            }
            
            return Ok(createProductCategoryViewModel);
        }
    }
}