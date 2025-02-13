using Server.UserRepository;
using Server.Domain;
using Server.Service;

namespace Server.UseCases
{
    public class UserUseCase
    {
        private readonly IUserRepository _repository;
        private readonly UserService _userService;

        public UserUseCase(IUserRepository repository, UserService userService)
        {
            _repository = repository;
            _userService = userService;
        }

        public void CreateUser(string username, string email, string password)
        {
            var user = new User
            {
                Username = username,
                Email = email,
                Password = password
            };

            _userService.ValidateUser(user);
            _repository.Add(user);
        }

        public User? GetUserByEmail(string email)
        {
            return _repository.GetByEmail(email);
        }

        public void UpdateUser(User user)
        {
            _userService.ValidateUser(user);
            _repository.Update(user);
        }

        public void DeleteUser(string email)
        {
            var user = _repository.GetByEmail(email);
            if (user == null)
                throw new ArgumentException("User not found.");

            _repository.Delete(user);
        }
    }
}