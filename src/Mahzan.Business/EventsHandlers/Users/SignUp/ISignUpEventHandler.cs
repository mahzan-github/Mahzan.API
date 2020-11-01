using Mahzan.Business.Events.Users.SignUp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mahzan.Business.EventsHandlers.Users.SignUp
{
    public interface ISignUpEventHandler
    {
        Task<Mahzan.Models.Entities.Users> HandleEvent(SignUpEvent signUpEvent);
    }
}
