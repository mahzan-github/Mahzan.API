using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using Mahzan.API.Application.Requests.Company;
using Mahzan.Business.V1.CommandHandlers.Company;
using Mahzan.Business.V1.Commands.Company;
using Mahzan.Persistance.V1.ViewModel.Company;
using Microsoft.AspNetCore.Authorization;

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
        
        [Authorize(AuthenticationSchemes = "Bearer")]
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
                    RFC = request.CompanyRequest.RFC,
                    CURP = request.CompanyRequest.CURP,
                    CommercialName = request.CompanyRequest.CommercialName,
                    BusinessName = request.CompanyRequest.BusinessName,
                    Email  = request.CompanyRequest.Email,
                    TaxRegimeCodeId = request.CompanyRequest.TaxRegimeCodeId,
                    OfficePhone = request.CompanyRequest.OfficePhone,
                    MobilePhone = request.CompanyRequest.MobilePhone,
                    AdditionalInformation = request.CompanyRequest.AdditionalInformation,
                    MemberId = new Guid(HttpContext.User.Claims.ToList()[1].Value)
                },
                CompanyAdressesCommand = request
                    .CompanyAdressesRequest
                    .Select(a => new CompanyAdressCommand
                    {
                        AdressType = a.AdressType,
                        Street = a.Street,
                        ExteriorNumber = a.ExteriorNumber,
                        InternalNumber = a.InternalNumber,
                        PostalCode = a.PostalCode
                    })
                    .ToList()
            });
            
            return Ok(response);
        }
    }
}
