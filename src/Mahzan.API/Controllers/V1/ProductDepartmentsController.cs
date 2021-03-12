using System;
using System.Net;
using System.Threading.Tasks;
using Mahzan.API.Application.Requests.ProductDepartmets;
using Mahzan.API.Exceptions;
using Mahzan.Persistance.V1.Dto.ProductDepartments;
using Mahzan.Persistance.V1.Repositories.ProductDepartments.CreateProductDepartment;
using Mahzan.Persistance.V1.ViewModel.ProductDepartments;
using Microsoft.AspNetCore.Mvc;

namespace Mahzan.API.Controllers.V1
{
    [ApiVersion("1")]
    [Route("v{version:apiVersion}")]
    [ApiController]
    public class ProductDepartmentsController : Controller
    {
        private readonly ICreateProductDepartment _createProductDepartment;

        public ProductDepartmentsController(
            ICreateProductDepartment createProductDepartment)
        {
            _createProductDepartment = createProductDepartment;
        }

        [HttpPost("product-department:create")]
        [ProducesResponseType(typeof(CreateProductDepartmentViewModel), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> Create(CreateProductDepartmentRequest request)
        {
            CreateProductDepartmentDto createProductDepartmentDto;
            
            try
            {
                createProductDepartmentDto = await _createProductDepartment
                    .Insert(new CreateProductDepartmentDto
                    {
                        CodeDepartment = request.CodeDepartment,
                        Name = request.Name,
                        CompanyId = request.CompanyId
                    });
            }
            catch (ArgumentException e)
            {
                throw new ServiceArgumentException(e);
            }
            
            return Ok(new CreateProductDepartmentViewModel
            {
                ProductDepartmentId = createProductDepartmentDto.ProductDepartmentId
            });
        }
    }
}