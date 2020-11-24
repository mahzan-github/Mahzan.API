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

namespace Mahzan.API.Controllers.V1
{
    [ApiVersion("1")]
    [Route("v{version:apiVersion}")]
    [ApiController]
    public class CompanyController : BaseController
    {

        private readonly ISaveCompanyEventHandler _saveCompanyEventHandler;

        public CompanyController(
            IMembersRepository membersRepository, 
            ISaveCompanyEventHandler saveCompanyEventHandler)
        : base(membersRepository)
        {
            _saveCompanyEventHandler = saveCompanyEventHandler;
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("company:save")]
        [ProducesResponseType(typeof(CompanyCreateViewModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Post(
            SaveCompanyCommand command)
        {
            try
            {
                Guid companyId = await _saveCompanyEventHandler
                    .Handler(new SaveCompanyEvent
                    {
                        CompanyEvent = new CompanyEvent {
                            RFC = command.CompanyCommand.RFC,
                            CURP = command.CompanyCommand.CURP,
                            CommercialName = command.CompanyCommand.CommercialName,
                            BusinessName = command.CompanyCommand.BusinessName,
                            Email = command.CompanyCommand.Email,
                            TaxRegimeCodeId = command.CompanyCommand.TaxRegimeCodeId,
                            OfficePhone = command.CompanyCommand.OfficePhone,
                            MobilePhone = command.CompanyCommand.MobilePhone,
                            AdditionalInformation = command.CompanyCommand.AdditionalInformation
                        },
                        CompanyAdressesEvent = command
                                               .CompanyAdressesCommand
                                               .Select(c => new CompanyAdressEvent {
                                                    AdressType = c.AdressType,
                                                    Street = c.Street,
                                                    ExteriorNumber = c.ExteriorNumber,
                                                    InternalNumber = c.InternalNumber,
                                                    PostalCode = c.PostalCode,
                                                    CompanyId = c.CompanyId
                                               })
                                               .ToList(),
                        MemberId = MemberId
                    });
            }
            catch (ArgumentException ex)
            {
                throw new ServiceArgumentException(ex);
            }
            catch (InvalidOperationException ex)
            {
                throw new ServiceInvalidOperationException(ex);
            }

            return Ok();
        }
    }
}
