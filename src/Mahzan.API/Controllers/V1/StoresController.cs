using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Mahzan.API.Application.Requests.Stores;
using Mahzan.Business.V1.CommandHandlers.Stores;
using Mahzan.Business.V1.Commands.Stores;
using Mahzan.Persistance.V1.ViewModel.Stores;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mahzan.API.Controllers.V1
{
    [ApiVersion("1")]
    [Route("v{version:apiVersion}")]
    [ApiController]
    public class StoresController : Controller
    {
        private readonly ICreateStoreCommandHandler _createStoreCommandHandler;

        public StoresController(
            ICreateStoreCommandHandler createStoreCommandHandler)
        {
            _createStoreCommandHandler = createStoreCommandHandler;
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("store:create")]
        [ProducesResponseType(typeof(CreateStoreViewModel), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> CreateSale(CreateStoreRequest request)
        {
            var response = await _createStoreCommandHandler
                .Handle(new CreateStoreCommand
                {
                    Name = request.Name,
                    Code = request.Code,
                    MemberId = new Guid(HttpContext.User.Claims.ToList()[1].Value)
                });
            
            return Ok(response);
        }
    }
}