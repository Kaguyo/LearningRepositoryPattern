namespace Server.Domain
{
    public class User() 
    {
        public string Username { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
        public string? ProfileImage { get; set; } = String.Empty;
    }
}