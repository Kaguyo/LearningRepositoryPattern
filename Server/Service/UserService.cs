using Server.Domain;

namespace Server.Service
{
    public class UserService
    {
        public void ValidateUser(User user)
        {
            if (string.IsNullOrWhiteSpace(user.Username))
                throw new ArgumentException("Username is required.");

            if (string.IsNullOrWhiteSpace(user.Email))
                throw new ArgumentException("Email is required.");

            if (string.IsNullOrWhiteSpace(user.Password))
                throw new ArgumentException("Password is required.");

            if (user.Password.Length < 6)
                throw new ArgumentException("Password must be at least 6 characters long.");
        }
    }

}