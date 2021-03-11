using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Mahzan.API.Application.Requests.TaxRegimeCodes;
using Mahzan.API.Application.Responses.TaxRegimeCodes;
using Mahzan.Persistance.V1.Filters.TaxRegimeCodes;
using Mahzan.Persistance.V1.Repositories.TaxRegimeCodes.GetTaxRegimeCodes;
using Mahzan.Persistance.V1.ViewModel.TaxRegimeCodes;


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
        [ProducesResponseType(typeof(GetTaxRegimeCodesResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get([FromQuery] GetTaxRegimeCodesRequest request)
        {
            GetTaxRegimeCodesResponse getTaxRegimeCodesResponse = null;

            var result= await _getTaxRegimeCodesRepository
                .FindAll(new GetTaxRegimeCodesFilter
                {
                    Code = request.Code
                });

            getTaxRegimeCodesResponse = new GetTaxRegimeCodesResponse
            {
                ListTaxRegimeCodesResponse = result
                    .Select(t => new TaxRegimeCodesResponse()
                    {
                        Code = t.Code,
                        Description = t.Description
                    })
                    .ToList()
            };

            return Ok(getTaxRegimeCodesResponse);
        }
    }
}
