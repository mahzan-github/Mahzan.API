using System;
using System.Net;
using System.Threading.Tasks;
using Mahzan.API.Application.Requests.ProductPurchaseUnits;
using Mahzan.API.Exceptions;
using Mahzan.Persistance.V1.Dto.ProductPurchaseUnits;
using Mahzan.Persistance.V1.Repositories.ProductPurchaseUnits.CreateProductPurchaseUnit;
using Mahzan.Persistance.V1.ViewModel.ProductDepartments;
using Mahzan.Persistance.V1.ViewModel.ProductPurchaseUnits;
using Microsoft.AspNetCore.Mvc;

namespace Mahzan.API.Controllers.V1
{
    [ApiVersion("1")]
    [Route("v{version:apiVersion}")]
    [ApiController]
    public class ProductPurchaseUnitsController : Controller
    {
        private readonly ICreateProductPurchaseUnitRepository _createProductPurchaseUnitRepository;

        public ProductPurchaseUnitsController(
            ICreateProductPurchaseUnitRepository createProductPurchaseUnitRepository)
        {
            _createProductPurchaseUnitRepository = createProductPurchaseUnitRepository;
        }

        [HttpPost("product-purchase-units:create")]
        [ProducesResponseType(typeof(CreateProductPurchaseUnitViewModel), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> CreateProductPurchaseUnit(CreateProductPurchaseUnitRequest request)
        {
            CreateProductPurchaseUnitDto createProductPurchaseUnitDto;

            try
            {
                createProductPurchaseUnitDto = await _createProductPurchaseUnitRepository
                    .Insert(new CreateProductPurchaseUnitDto
                    {
                        Abbreviation = request.Abbreviation,
                        Description = request.Description,
                        CompanyId = request.CompanyId
                    });
            }
            catch (ArgumentException e)
            {
                throw new ServiceArgumentException(e);
            }

            
            return Ok(new CreateProductPurchaseUnitViewModel
            {
                ProductPurchaseUnitId = createProductPurchaseUnitDto.ProductPurchaseUnitId
            });
        }
    }
}