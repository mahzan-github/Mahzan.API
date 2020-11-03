using Mahzan.Business.Events.Users.SignUp;
using Mahzan.Business.EventsServices.Email;
using Mahzan.Business.Exceptions.Users.SignUp;
using Mahzan.Dapper.DTO.Users.SignUp;
using Mahzan.Dapper.Repositories.Users.SignUp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mahzan.Business.EventsHandlers.Users.SignUp
{
    public class SignUpEventHandler : ISignUpEventHandler
    {
        #region Private  Methods

        private readonly ISignUpRepository _signUpRepository;

        private readonly IEmailSender _emailSender;

        #endregion

        #region Constructors

        public SignUpEventHandler(
            ISignUpRepository signUpRepository,
            IEmailSender emailSender)
        {
            _signUpRepository = signUpRepository;
            _emailSender = emailSender;
        }

        #endregion 

        #region Public  Methods

        public async Task<Models.Entities.Users> HandleEvent(SignUpEvent signUpEvent)
        {

            Models.Entities.Users user = await _signUpRepository
                .HandleRepository(new SignUpDto
                {
                    Name = signUpEvent.Name,
                    Phone = signUpEvent.Phone,
                    Email = signUpEvent.Email,
                    UserName = signUpEvent.UserName,
                    Password = signUpEvent.Password
                });

            if (user==null)
            {
                throw new SignUpArgumentNullException(
                    $"No se logró dar de alta el usuario {signUpEvent.Name}, no se enviara el correo de registro."
                    );
            }

            //Envio de Correo
            await SendEmail(user);

            return user;
        }

        #endregion

        #region Private Methods

        private async Task SendEmail(Models.Entities.Users user) 
        {
            await _emailSender
            .SendEmailAsync(
            user.Email,
            "Confirma tu Email",
            $"Por favor confirma tu email, haz click <a href='http://159.203.81.150:5000/v1/users/confirm-email?userId={user.UserId}&tokenConfrimEmail={user.TokenConfirmEmail}'>aquí</a>");

        }

        #endregion

    }
}
