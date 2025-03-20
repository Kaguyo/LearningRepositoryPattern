using System.Text.Json.Serialization;

namespace FGOxSTR_Server.Domain
{
    public class User() 
    {

        public string Username { get; set; } = String.Empty;
        
        public string Email { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
        public byte[]? ProfileImage { get; set; }
        public int Id { get; set; }

        public override string ToString()
        {
            return $"ID: {Id}, Name: {Username}, Email: {Email}";
        }
    }
}