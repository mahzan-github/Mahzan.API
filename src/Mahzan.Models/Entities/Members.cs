using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Models.Entities
{
    public class Members
    {
        public Guid MemberId { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public Guid UserId { get; set; }

        public Guid? MemberPatternId { get; set; }
    }
}
