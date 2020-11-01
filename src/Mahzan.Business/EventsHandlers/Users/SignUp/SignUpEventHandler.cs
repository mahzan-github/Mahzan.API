using Mahzan.Business.Events.Users.SignUp;
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
        private readonly ISignUpRepository _signUpRepository;

        public SignUpEventHandler(
            ISignUpRepository signUpRepository)
        {
            _signUpRepository = signUpRepository;
        }

        public async Task<Models.Entities.Users> HandleEvent(SignUpEvent signUpEvent)
        {

            Mahzan.Models.Entities.Users user = await _signUpRepository
                .HandleRepository(new SignUpDto
                {
                    Name = signUpEvent.Name,
                    Phone = signUpEvent.Phone,
                    Email = signUpEvent.Email,
                    UserName = signUpEvent.UserName,
                    Password = signUpEvent.Password
                });


            return user;
        }
    }
}
