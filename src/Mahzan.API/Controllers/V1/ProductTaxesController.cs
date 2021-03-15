using System;
using System.Net;
using System.Threading.Tasks;
using Mahzan.API.Application.Requests.ProductTaxes;
using Mahzan.API.Exceptions;
using Mahzan.Persistance.V1.Dto.ProductTaxes;
using Mahzan.Persistance.V1.Repositories.ProductTaxes.CreateProductTax;
using Mahzan.Persistance.V1.ViewModel.ProductTaxes;
using Microsoft.AspNetCore.Mvc;

namespace Mahzan.API.Controllers.V1
{
    [ApiVersion("1")]
    [Route("v{version:apiVersion}")]
    [ApiController]
    public class ProductTaxesController : Controller
    {
        private ICreateProductTaxRepository _createProductTaxRepository;

        public ProductTaxesController(
            ICreateProductTaxRepository createProductTaxRepository)
        {
            _createProductTaxRepository = createProductTaxRepository;
        }


        [HttpPost("product-tax:create")]
        [ProducesResponseType(typeof(CreateProductTaxViewModel), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> CreateProductPurchaseUnit(
            CreateProductTaxRequest request)
        {
            CreateProductTaxDto createProductTaxDto;
            
            try
            {
                createProductTaxDto = await _createProductTaxRepository
                    .Insert(new CreateProductTaxDto
                    {
                        Name = request.Name,
                        Percentage = request.percentage,
                        PrintOnTicket = request.PrintOnTicket,
                        CompanyId = request.CompanyId
                    });
            }
            catch (ArgumentException ex)
            {
                throw new ServiceArgumentException(ex);
            }
            return Ok(new CreateProductTaxViewModel
            {
                ProductTaxId = createProductTaxDto.ProductTaxId
            });
        }
    }
}