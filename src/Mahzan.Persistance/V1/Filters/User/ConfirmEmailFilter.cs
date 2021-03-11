using System;

namespace Mahzan.Persistance.V1.Filters.User
{
    public class ConfirmEmailFilter
    {
        public string UserId { get; set; }
        
        public string TokenConfrimEmail { get; set; }
    }
}