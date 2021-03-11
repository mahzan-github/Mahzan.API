using System.Threading.Tasks;
using Mahzan.Business.EventsServices.Email;
using Mahzan.Business.V1.Commands.User;
using Mahzan.Persistance.V1.Dto.User;
using Mahzan.Persistance.V1.Repositories.User;
using Mahzan.Persistance.V1.ViewModel.User;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Mahzan.Business.V1.CommandHandlers.User
{
    public class SignUpCommandHandler
    :CommandHandlerBase<CreateNewUserCommand,CreateNewUserViewModel>, ISignUpCommandHandler
    {
        private readonly ISignUpRepository _signUpRepository;
        
        private readonly IEmailSender _emailSender;
        
        public SignUpCommandHandler(
            NpgsqlConnection connection,
            IEmailSender emailSender, 
            ISignUpRepository signUpRepository,
            ILogger<CommandHandlerBase<CreateNewUserCommand,CreateNewUserViewModel>> logger) 
            : base(connection, logger)
        {
            _emailSender = emailSender;
            _signUpRepository = signUpRepository;
        }

        protected override async Task<CreateNewUserViewModel> HandleTransaction(
            CreateNewUserCommand command)
        {
            var userInserted = await _signUpRepository
                .Insert(new SignUpDto
                {
                    Name = command.Name,
                    Phone = command.Phone,
                    Email   = command.Email,
                    UserName = command.UserName,
                    Password = command.Password
                });

            //await SendEmail(userInserted);
            
            return CreateNewUserViewModel.From(userInserted);
        }
        
        #region :: Private Methods ::

        private async Task SendEmail(SignUpDto signUpDto) 
        {
            await _emailSender
                .SendEmailAsync(
                    signUpDto.Email,
                    "Confirma tu Email",
                    $"Por favor confirma tu email, haz click <a href='http://159.203.81.150:5000/v1/users/confirm-email?userId={signUpDto.UserId}&tokenConfrimEmail={signUpDto.TokenConfirmEmail}'>aquí</a>");

        }

        #endregion
    }
}