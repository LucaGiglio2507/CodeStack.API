namespace CodeStack.API.Dtos.Responses
{
    public class LoginResponseDto
    {
        public required string Token { get; set; }
        public bool IsPasswordChanged { get; set; }
        public bool IsActive { get; set; }
    }
}
