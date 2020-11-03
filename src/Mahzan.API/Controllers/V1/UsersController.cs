using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Mahzan.API.Application.Commands.UsersController;
using Mahzan.API.Exceptions;
using Mahzan.API.ViewModels.UsersController;
using Mahzan.Business.Events.Users.SignUp;
using Mahzan.Business.EventsHandlers.Users.SignUp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Mahzan.API.Controllers.V1
{
    [ApiVersion("1")]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly ISignUpEventHandler _signUpEventHandler;

        private readonly ILogger<UsersController> _logger;

        public UsersController(
            ISignUpEventHandler signUpEventHandler, 
            ILogger<UsersController> logger)
        {
            _signUpEventHandler = signUpEventHandler;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("sign-up")]
        [ProducesResponseType(typeof(SignUpViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> SignUp([FromBody] RegistroCommand command)
        {

            SignUpViewModel signUpViewModel = null;

            try
            {
                _logger.LogInformation($"Registro de Usuario {command.UserName}");

                Models.Entities.Users user = await _signUpEventHandler
                    .HandleEvent(new SignUpEvent
                    {
                        Name = command.Name,
                        Phone = command.Phone,
                        Email = command.Email,
                        UserName = command.UserName,
                        Password = command.Password
                    });

                if (user!=null)
                {
                    signUpViewModel = new SignUpViewModel
                    {
                        UserId = user.UserId
                    };
                }


            }
            catch (ServiceInvalidOperationException ex)
            {
                throw new ServiceInvalidOperationException(ex);
            }
            catch (ServiceArgumentException ex)
            {

                throw new ServiceArgumentException(ex);
            }

            return Ok(signUpViewModel);
        }
    }
}
