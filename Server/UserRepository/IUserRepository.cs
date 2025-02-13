using Server.Domain;

namespace Server.UserRepository
{
    public interface IUserRepository
    {
        void Add(User user);
        User? GetByEmail(string email);
        void Update(User user);
        void Delete(User user);
    }
}
