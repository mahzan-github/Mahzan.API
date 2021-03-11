using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Mahzan.API.Application.Commands.UsersController;
using Mahzan.API.Application.Requests.User;
using Mahzan.API.Exceptions;
using Mahzan.API.ViewModels.UsersController;
using Mahzan.Business.Events.Users.LogIn;
using Mahzan.Business.Events.Users.SignUp;
using Mahzan.Business.EventsHandlers.Users.LogIn;
using Mahzan.Business.V1.CommandHandlers.User;
using Mahzan.Business.V1.Commands.User;
using Mahzan.Persistance.V1.Dto.User;
using Mahzan.Persistance.V1.Filters.User;
using Mahzan.Persistance.V1.Repositories.User.ConfirmEmail;
using Mahzan.Persistance.V1.ViewModel.User;
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
        private readonly ISignUpCommandHandler _signUpCommandHandler;

        private readonly IConfirmEmailRepository _confirmEmailRepository;
        
        //private readonly IConfirmEmailRepository _confirmEmailRepository;

        //private readonly ILoginEventHandler _loginEventHandler;

        private readonly ILogger<UserController> _logger;

        public UserController(
            ISignUpCommandHandler signUpCommandHandler,
            IConfirmEmailRepository confirmEmailRepository,
            ILogger<UserController> logger)
        {
            //Events Handlers
            //_loginEventHandler = loginEventHandler;

            //Repositories
            //_confirmEmailRepository = confirmEmailRepository;

            //Logger
            _confirmEmailRepository = confirmEmailRepository;
            _signUpCommandHandler = signUpCommandHandler;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("user:sign-up")]
        [ProducesResponseType(typeof(SignUpViewModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> SignUp(
            [FromBody] SignUpRequest request)
        {
            SignUpViewModel signUpViewModel = null;
            try
            {
                _logger.LogInformation($"Registro de Usuario {request.UserName}");

                signUpViewModel = await _signUpCommandHandler
                    .Handle(new CreateNewUserCommand
                    {
                        Name = request.Name,
                        Phone = request.Phone,
                        Email = request.Email,
                        UserName = request.UserName,
                        Password = request.Password
                    });
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
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> ConfirmEmail(string userId, string tokenConfrimEmail)
        {
            ConfirmEmailDto confirmEmailDto = null;
            
            try
            {
                confirmEmailDto = await _confirmEmailRepository
                    .Update(new ConfirmEmailDto()
                    {
                        UserId = userId,
                        TokenConfrimEmail = tokenConfrimEmail
                    });

            }
            catch (InvalidOperationException e)
            {
                throw new ServiceInvalidOperationException(e);
            }
            catch (ArgumentException e)
            {
                throw new ServiceArgumentException(e);
            }

            return Ok($"Se ha confirmado correctamente el usuario {confirmEmailDto.UserName}.");
        }


        // [AllowAnonymous]
        // [HttpGet("user:confirm-email")]
        // [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        // public async Task<IActionResult> ConfirmEmail(string userId, string tokenConfrimEmail)
        // {
        //     try
        //     {
        //         await _confirmEmailRepository
        //             .HandleRepository(new Guid(userId), new Guid(tokenConfrimEmail));
        //     }
        //     catch (ServiceInvalidOperationException ex)
        //     {
        //
        //         throw new ServiceInvalidOperationException(ex);
        //     }
        //
        //     return Ok("Se ha confirmado correctamente tu Email.");
        // }
        //
        // [AllowAnonymous]
        // [HttpGet("user:sign-in")]
        // [ProducesResponseType(typeof(LogInViewModel), (int)HttpStatusCode.OK)]
        //
        // public async Task<IActionResult> SignIn(string userName, string passowrd)
        // {
        //
        //     LogInViewModel logInViewModel = null;
        //     try
        //     {
        //         string token = await _loginEventHandler
        //             .HandleEvent(new LoginEvent
        //             {
        //                 UserName = userName,
        //                 Password = passowrd
        //             });
        //
        //         if (token!=null)
        //         {
        //             logInViewModel = new LogInViewModel
        //             {
        //                 Token = token
        //             };
        //         }
        //     }
        //     catch (ArgumentException ex)
        //     {
        //         throw new ServiceArgumentException(ex);
        //     }
        //     catch (InvalidOperationException ex)
        //     {
        //         throw new ServiceInvalidOperationException(ex);
        //     }
        //
        //     return Ok(logInViewModel);
        // }
    }
}
