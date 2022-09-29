using DevFreela.Core.Entities;

namespace DevFreela.Core.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int userId);
        Task AddAsync(User user);
        Task SaveChangesAsync();

        Task<User?> GetUserByEmailAndPasswordAsync(string email, string password);
    }
}
