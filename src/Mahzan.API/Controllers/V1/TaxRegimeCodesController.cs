using Mahzan.API.Application.Filters.TaxRegimeCodesController;
using Mahzan.API.ViewModels.TaxRegimeCodesController;
using Mahzan.Dapper.V1.Repositories.TaxRegimeCodes.GetTaxRegimeCodes;
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
    public class TaxRegimeCodesController : ControllerBase
    {
        private readonly IGetTaxRegimeCodesRepository _getTaxRegimeCodesRepository;

        public TaxRegimeCodesController(
            IGetTaxRegimeCodesRepository getTaxRegimeCodesRepository)
        {
            _getTaxRegimeCodesRepository = getTaxRegimeCodesRepository;
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("companies:get")]
        [ProducesResponseType(typeof(List<GetTaxRegimeCodesViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get([FromQuery] GetTaxRegimeCodesFilter filter) 
        {
            List<Models.Entities.TaxRegimeCodes> taxRegimeCodes;
            List<GetTaxRegimeCodesViewModel> getTaxRegimeCodesViewModel;
            try
            {
                taxRegimeCodes = await _getTaxRegimeCodesRepository
                    .GetTaxRegimeCodes(new Dapper.V1.Filters.TaxRegimeCodes.GetTaxRegimeCodes.GetTaxRegimeCodesFilter
                    {
                        Code = filter.Code
                    });

                getTaxRegimeCodesViewModel = taxRegimeCodes
                    .Select(t => new GetTaxRegimeCodesViewModel
                    {
                        TaxRegimeCodeId = t.TaxRegimeCodeId,
                        Code = t.Code,
                        Description = t.Description
                    }).ToList();
            }
            catch (Exception)
            {

                throw;
            }


            return Ok(getTaxRegimeCodesViewModel);
        }
    }
}
