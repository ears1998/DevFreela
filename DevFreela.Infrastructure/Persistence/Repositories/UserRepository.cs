using DevFreela.Core.Entities;
using DevFreela.Core.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DevFreelaDbContext _dbContext;

        public UserRepository(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<User>> GetAllAsync()
        {
            var users = await _dbContext.Users
                            .Include(u => u.FreelanceProjects)
                            .Include(u => u.OwnedProjects)
                            .ToListAsync();
            return users;
        }

        public async Task<User?> GetByIdAsync(int userId)
        {
            var user = await _dbContext.Users
                            .Include(u => u.Id)
                            .Include(u => u.FreelanceProjects)
                            .Include(u => u.OwnedProjects)
                            .SingleOrDefaultAsync(u => u.Id == userId);

            return user;
        }

        public async Task AddAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);

            await _dbContext.SaveChangesAsync();
        }

        public async Task SaveChangesAsync() => await _dbContext.SaveChangesAsync();

        public async Task<User?> GetUserByEmailAndPasswordAsync(string email, string password)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Email == email && u.Password == password);

            return user;
        }
    }
}
