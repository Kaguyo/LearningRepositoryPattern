using FGOxSTR_Server.Domain;

namespace FGOxSTR_Server.Repository
{   
    public interface IUserRepository
    {
        Task AddAsync(User user);
        Task<User?> GetByEmailAsync(string email);

        Task AddUserInMemory(User user);
        User? GetByEmailInMemory(string email);

        int GetUserId();
    }

}
