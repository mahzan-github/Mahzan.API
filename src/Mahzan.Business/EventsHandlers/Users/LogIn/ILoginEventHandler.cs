using Mahzan.Business.Events.Users.LogIn;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mahzan.Business.EventsHandlers.Users.LogIn
{
    public interface ILoginEventHandler
    {
        Task<string> HandleEvent(LoginEvent loginEvent);
    }
}
