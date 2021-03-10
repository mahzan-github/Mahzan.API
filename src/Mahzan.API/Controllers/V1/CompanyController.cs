using Mahzan.API.Application.Commands.CompanyController;
using Mahzan.API.Controllers._Base;
using Mahzan.API.Exceptions;
using Mahzan.API.ViewModels.CompanyController;
using Mahzan.Business.V1.Events.Company;
using Mahzan.Business.V1.EventsHandlers.Company.SaveCompany;
using Mahzan.Dapper.V1.Repositories._Base.Members;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
           var response= await _createCompanyCommandHandler.Handle(new CreateCompanyCommand
            {

            });
            
            return Ok(response);
        }
    }
}
