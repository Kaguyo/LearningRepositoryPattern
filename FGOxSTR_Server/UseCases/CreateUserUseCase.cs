using FGOxSTR_Server.Domain;
using FGOxSTR_Server.Repository;
using FGOxSTR_Server.Data;
using Microsoft.EntityFrameworkCore;

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
                Password = password,
                Id = _context.GetUserCount()
            };

            await _userRepository.AddAsync(user);

            return user;
        }
    }
    
}