using DevFreela.Core.Entities;
using DevFreela.Core.Repositories.Interfaces;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class ProjectCommentsRepository : IProjectCommentsRepository
    {

        private readonly DevFreelaDbContext _dbContext;

        public ProjectCommentsRepository(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(ProjectComment comment)
        {
            await _dbContext.ProjectComments.AddAsync(comment);

            await _dbContext.SaveChangesAsync();
        }
    }
}
