using Server.UserRepository;
using Server.Domain;


namespace Server.UseCases
{
    public class UserUseCase
    {
        
        private readonly IUserRepository _repository;

        public UserUseCase(IUserRepository repository)
        {
            _repository = repository;
        }

        public void CreateUser(string username, string email, string password)
        {
            var user = new User
            {
                Username = username,
                Email = email,
                Password = password
            };

            ValidateUser(user);
            _repository.Add(user);
        }

        public User? GetUserByEmail(string email)
        {
            return _repository.GetByEmail(email);
        }

        public void UpdateUser(User user)
        {
            ValidateUser(user);
            _repository.Update(user);
        }

        public void DeleteUser(string email)
        {
            var user = _repository.GetByEmail(email);
            if (user == null)
                throw new ArgumentException("User not found.");

            _repository.Delete(user);
        }

        private void ValidateUser(User user)
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