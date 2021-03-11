using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using Mahzan.API.Application.Requests.Company;
using Mahzan.Business.V1.CommandHandlers.Company;
using Mahzan.Business.V1.Commands.Company;
using Mahzan.Persistance.V1.ViewModel.Company;

namespace Mahzan.API.Controllers.V1
{
    [ApiVersion("1")]
    [Route("v{version:apiVersion}")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICreateCompanyCommandHandler _createCompanyCommandHandler;
        public CompanyController(
            ICreateCompanyCommandHandler createCompanyCommandHandler)
        {
            _createCompanyCommandHandler = createCompanyCommandHandler;
        }
        
        //[Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("company:create")]
        [ProducesResponseType(typeof(CreateCompanyViewModel), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> Create(
            CreateCompanyRequest request)
        {
           var response= await _createCompanyCommandHandler
               .Handle(new CreateCompanyCommand
            {
                CompanyCommand = new CompanyCommand
                {
                    RFC = request.CompanyRequest.RFC
                }
            });
            
            return Ok(response);
        }
    }
}
