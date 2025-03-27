using FGOxSTR_Server.Domain;
using FGOxSTR_Server.Repository;
using FGOxSTR_Server.Data;
using Microsoft.EntityFrameworkCore;
using Isopoh.Cryptography.Argon2;

namespace FGOxSTR_Server.UseCases
{
    public class CreateUserUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly ApplicationDbContext _context;
        public CreateUserUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<User> Execute(string username, string email, string password)
        {
            var existingUser = await _userRepository.GetByEmailAsync(email);
            if (existingUser != null)
            {
                throw new Exception("O Email já está em uso.");
            }

            var user = new User
            {
                Username = username,
                Email = email,
                Password = Argon2.Hash(password),
                Id = _context.GetUserId() + 60000000
            };

            await _userRepository.AddAsync(user);

            return user;
        }
    }
    
}