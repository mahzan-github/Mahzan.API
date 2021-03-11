using System;

namespace Mahzan.Persistance.V1.Dto.User
{
    public record LogInDto
    {
        public Guid MemberId { get; init; }
        public string UserName { get; set; }
        
        public string Password { get; set; }
    }
}