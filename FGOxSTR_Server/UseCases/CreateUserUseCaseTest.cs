using System.Numerics;
using FGOxSTR_Server.Domain;
using FGOxSTR_Server.Repository;
using Isopoh.Cryptography.Argon2;

namespace FGOxSTR_Server.UseCases
{
    public class CreateUserUseCaseTest
    {
        private readonly IUserRepository _userRepository;

        public CreateUserUseCaseTest(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<User> Execute(string username, string email, string password)
        {
            User? existingUser = _userRepository.GetByEmailInMemory(email);
            if (existingUser != null)
            {
                throw new Exception("O Email já está em uso.");
            }

            var user = new User
            {
                Username = username,
                Email = email,
                Password = Argon2.Hash(password),
                Id = _userRepository.GetUserId() + 60000000
            };

            await _userRepository.AddUserInMemory(user);

            return user;
        }
    }
}