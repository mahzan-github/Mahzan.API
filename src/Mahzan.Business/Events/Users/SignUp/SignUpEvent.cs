using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Business.Events.Users.SignUp
{
    public class SignUpEvent
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
