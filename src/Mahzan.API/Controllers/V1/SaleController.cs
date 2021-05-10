using System.Net;
using System.Threading.Tasks;
using Mahzan.API.Application.Requests.Sales;
using Mahzan.Business.V1.CommandHandlers.Sales;
using Mahzan.Business.V1.Commands.Sales;
using Mahzan.Persistance.V1.ViewModel.Sales;
using Microsoft.AspNetCore.Mvc;

namespace Mahzan.API.Controllers.V1
{
    [ApiVersion("1")]
    [Route("v{version:apiVersion}")]
    [ApiController]
    public class SaleController : Controller
    {
        private readonly ICreateSaleCommandHandler _createSaleCommandHandler;
        
        public SaleController(
            ICreateSaleCommandHandler createSaleCommandHandler)
        {
            _createSaleCommandHandler = createSaleCommandHandler;
        }

        [HttpPost("sale:create")]
        [ProducesResponseType(typeof(CreateSaleViewModel), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> CreateSale(CreateSaleRequest request)
        {

            var res = await _createSaleCommandHandler
                .Handle(new CreateSaleCommand());
            
            return Ok();
        }
    }
}