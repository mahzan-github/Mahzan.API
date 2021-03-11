namespace Mahzan.Persistance.V1.Dto.User
{
    public record ConfirmEmailDto
    {
        public string UserId { get; set; }
        public string UserName { get; init; }
        public string TokenConfrimEmail { get; set; }
        public bool ConfirmEmail { get; init; }
    }
}