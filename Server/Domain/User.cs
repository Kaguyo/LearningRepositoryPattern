using System.ComponentModel.DataAnnotations;

namespace Server.Domain
{
    public class User() 
    {
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; } = String.Empty;

        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; } = String.Empty;

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; } = String.Empty;

        public string? ProfileImage { get; set; } = String.Empty;

    }
}