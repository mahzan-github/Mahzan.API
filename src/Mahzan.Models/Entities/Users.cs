using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Models.Entities
{
    public class Users
    {
        public Guid UserId { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public bool Active { get; set; }

        public bool ConfirmEmail { get; set; }

        public Guid TokenConfirmEmail { get; set; }

        public Guid? UserPatternId { get; set; }

        public string Email { get; set; }
    }
}
