namespace Mahzan.Business.V1.Commands.User
{
    public class CreateNewUserCommand
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}