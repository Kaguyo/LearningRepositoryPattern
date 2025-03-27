using FGOxSTR_Server.Domain;
using FGOxSTR_Server.Repository;
using Isopoh.Cryptography.Argon2;

namespace FGOxSTR_Server.UseCases
{
    public class LoginUserUseCaseTest
    {
        private readonly IUserRepository _userRepository;

        public LoginUserUseCaseTest(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User? Execute(string email, string password)
        {
            var user = _userRepository.GetByEmailInMemory(email);
            if (user == null)
            {
                return null;
            }

            if (!Argon2.Verify(user.Password, password))
            {
                return null;
            }


            return user;
        }
    }
}
