using FGOxSTR_Server.Domain;
using FGOxSTR_Server.Repository;
using Isopoh.Cryptography.Argon2;

namespace FGOxSTR_Server.UseCases
{
    public class LoginUserUseCase
    {
        private readonly IUserRepository _userRepository;

        public LoginUserUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User?> Execute(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);
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
