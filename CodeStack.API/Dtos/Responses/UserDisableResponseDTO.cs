namespace CodeStack.API.Dtos.Responses
{
    public class UserDisableResponseDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public bool IsActivated { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Message { get; set; } = "Le compte a été désactivé.";
    }
}
