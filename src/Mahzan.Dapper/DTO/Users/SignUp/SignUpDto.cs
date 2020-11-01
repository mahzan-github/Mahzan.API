using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Dapper.DTO.Users.SignUp
{
    public class SignUpDto
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
