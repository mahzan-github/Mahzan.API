using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Mahzan.API.Exceptions;
using Mahzan.Persistance.V1.Filters.MenuRole;
using Mahzan.Persistance.V1.Repositories.MenuRole.GetAside;
using Mahzan.Persistance.V1.ViewModel.MenuRole;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mahzan.API.Controllers.V1
{
    [ApiVersion("1")]
    [Route("v{version:apiVersion}")]
    [ApiController]
    public class MenuController : Controller
    {
        private readonly IGetAsideRepository _getAsideRepository;

        public MenuController(IGetAsideRepository getAsideRepository)
        {
            _getAsideRepository = getAsideRepository;
        }


        [Authorize]
        [HttpGet("menu:get-aside")]
        [ProducesResponseType(typeof(GetMenuRoleViewModel), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> GetAside()
        {
            GetMenuRoleViewModel getMenuRoleViewModel = new GetMenuRoleViewModel();
            
            try
            {
                getMenuRoleViewModel = await _getAsideRepository
                    .FindSingle(new GetMenuRoleFilter()
                    {
                        UserId = new Guid(HttpContext.User.Claims.ToList()[0].Value) 
                    });
            }
            catch (ArgumentException e)
            {
                throw new ServiceArgumentException(e);
            }
            
            return Ok(getMenuRoleViewModel);
        }
    }
}