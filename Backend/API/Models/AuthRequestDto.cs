namespace API.Models
{
    public class AuthRequestDto
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
    }
}
