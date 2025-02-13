using Server.Domain;

namespace Server.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users = new();

        public void Add(User user)
        {
            _users.Add(user);
        }

        public User? GetByEmail(string email)
        {
            return _users.FirstOrDefault(u => u.Email == email);
        }

        public void Update(User user)
        {
            var existingUser = GetByEmail(user.Email);
            if (existingUser != null)
            {
                existingUser.Username = user.Username;
                existingUser.Password = user.Password;
                existingUser.ProfileImage = user.ProfileImage;
            }
        }

        public void Delete(User user)
        {
            _users.Remove(user);
        }
    }
}