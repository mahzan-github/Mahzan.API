using System;
using Mahzan.Persistance.V1.Dto.User;

namespace Mahzan.Persistance.V1.ViewModel.User
{
    public class CreateNewUserViewModel
    {
        public Guid UserId { get; set; }
        
        public static CreateNewUserViewModel From(SignUpDto signUpDto)
        {
            return new ()
            {
                UserId = signUpDto.UserId
            };
        }
    }
}