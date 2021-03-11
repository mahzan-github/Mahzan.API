using System;
using Mahzan.Persistance.V1.Dto.User;

namespace Mahzan.Persistance.V1.ViewModel.User
{
    public class SignUpViewModel
    {
        public Guid UserId { get; set; }
        
        public static SignUpViewModel From(SignUpDto signUpDto)
        {
            return new ()
            {
                UserId = signUpDto.UserId
            };
        }
    }
}