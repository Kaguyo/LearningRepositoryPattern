using FGOxSTR_Server.Domain;
using FGOxSTR_Server.Data;
using Microsoft.EntityFrameworkCore;

namespace FGOxSTR_Server.Repository
{   
    public class UserRepository : IUserRepository
    {
        static private readonly List<User> _users = new();

        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        // Operacoes de persistencia em memoria para testes
        public Task AddUserInMemory(User user)
        {
            _users.Add(user);
            foreach (User usuario in _users)
            {
                Console.WriteLine(usuario);
            }
            return Task.CompletedTask;
        }


        public User? GetByEmailInMemory(string email)
        {
            return _users.FirstOrDefault(user => user.Email == email); 
        }

    }
}
