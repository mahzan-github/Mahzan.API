using System;
using System.Net;
using System.Threading.Tasks;
using Mahzan.API.Application.Requests.ProductSaleUnits;
using Mahzan.Persistance.V1.Dto.ProductSaleUnits;
using Mahzan.Persistance.V1.Repositories.ProductSaleUnits.CreateProductSaleUnit;
using Mahzan.Persistance.V1.ViewModel.ProductSaleUnits;
using Microsoft.AspNetCore.Mvc;

namespace Mahzan.API.Controllers.V1
{
    [ApiVersion("1")]
    [Route("v{version:apiVersion}")]
    [ApiController]
    public class ProductSaleUnitsController : Controller
    {
        private readonly ICreateProductSaleUnitRepository _createProductSaleUnitRepository;

        public ProductSaleUnitsController(
            ICreateProductSaleUnitRepository createProductSaleUnitRepository)
        {
            _createProductSaleUnitRepository = createProductSaleUnitRepository;
        }


        [HttpPost("product-sale-units:create")]
        [ProducesResponseType(typeof(CreateProductSaleUnitViewModel), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> CreateProductPurchaseUnit(
            CreateProductSaleUnitRequest request)
        {
            CreateProductSaleUnitDto createProductSaleUnitDto;
            try
            {
                createProductSaleUnitDto =await _createProductSaleUnitRepository
                    .Insert(new CreateProductSaleUnitDto
                    {
                        Abbreviation = request.Abbreviation,
                        Description = request.Description,
                        CompanyId = request.CompanyId
                    });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return Ok(new CreateProductSaleUnitViewModel
            {
                ProductSaleUnitId = createProductSaleUnitDto.ProductSaleUnitId
            });
        }
    }
}