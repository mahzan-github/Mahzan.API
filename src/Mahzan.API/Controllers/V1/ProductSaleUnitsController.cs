using System.Net;
using System.Threading.Tasks;
using Mahzan.API.Application.Requests.ProductSaleUnits;
using Mahzan.Persistance.V1.ViewModel.ProductSaleUnits;
using Microsoft.AspNetCore.Mvc;

namespace Mahzan.API.Controllers.V1
{
    [ApiVersion("1")]
    [Route("v{version:apiVersion}")]
    [ApiController]
    public class ProductSaleUnitsController : Controller
    {
        [HttpPost("product-sale-units:create")]
        [ProducesResponseType(typeof(CreateProductSaleUnitViewModel), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> CreateProductPurchaseUnit(CreateProductSaleUnitRequest request)
        {

            return Ok();
        }
    }
}