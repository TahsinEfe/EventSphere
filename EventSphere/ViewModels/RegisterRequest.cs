namespace EventSphere.ViewModels
{
    public class RegisterRequest
    {
        public string Username { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; } = null!;
    }
}
