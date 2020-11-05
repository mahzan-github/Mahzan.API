using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Mahzan.API.Application.Commands.UsersController;
using Mahzan.API.Exceptions;
using Mahzan.API.ViewModels.UsersController;
using Mahzan.Business.Events.Users.LogIn;
using Mahzan.Business.Events.Users.SignUp;
using Mahzan.Business.EventsHandlers.Users.LogIn;
using Mahzan.Business.EventsHandlers.Users.SignUp;
using Mahzan.Dapper.Repositories.Users.ConfirmEmail;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Mahzan.API.Controllers.V1
{
    [ApiVersion("1")]
    [Route("v{version:apiVersion}")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly ISignUpEventHandler _signUpEventHandler;

        private readonly IConfirmEmailRepository _confirmEmailRepository;

        private readonly ILoginEventHandler _loginEventHandler;

        private readonly ILogger<UserController> _logger;

        public UserController(
            ISignUpEventHandler signUpEventHandler,
            IConfirmEmailRepository confirmEmailRepository,
            ILoginEventHandler loginEventHandler,
            ILogger<UserController> logger)
        {
            //Events Handlers
            _signUpEventHandler = signUpEventHandler;
            _loginEventHandler = loginEventHandler;

            //Repositories
            _confirmEmailRepository = confirmEmailRepository;

            //Logger
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("user:sign-up")]
        [ProducesResponseType(typeof(SignUpViewModel), (int)HttpStatusCode.OK)]
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

        [AllowAnonymous]
        [HttpGet("user:confirm-email")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ConfirmEmail(string userId, string tokenConfrimEmail)
        {
            try
            {
                await _confirmEmailRepository
                    .HandleRepository(new Guid(userId), new Guid(tokenConfrimEmail));
            }
            catch (ServiceInvalidOperationException ex)
            {

                throw new ServiceInvalidOperationException(ex);
            }

            return Ok("Se ha confirmado correctamente tu Email.");
        }

        [AllowAnonymous]
        [HttpGet("user:sign-in")]
        [ProducesResponseType(typeof(LogInViewModel), (int)HttpStatusCode.OK)]
        
        public async Task<IActionResult> SignIn(string userName, string passowrd)
        {

            LogInViewModel logInViewModel = null;
            try
            {
                string token = await _loginEventHandler
                    .HandleEvent(new LoginEvent
                    {
                        UserName = userName,
                        Password = passowrd
                    });

                if (token!=null)
                {
                    logInViewModel = new LogInViewModel
                    {
                        Token = token
                    };
                }
            }
            catch (ArgumentException ex)
            {
                throw new ServiceArgumentException(ex);
            }
            catch (InvalidOperationException ex)
            {
                throw new ServiceInvalidOperationException(ex);
            }

            return Ok(logInViewModel);
        }
    }
}
