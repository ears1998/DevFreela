using DevFreela.Core.Entities;
using DevFreela.Core.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DevFreelaDbContext _dbContext;
        public ProjectRepository(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Project>> GetAllAsync(string query)
        {
            var projects = new List<Project>();

            if (query != null && query != string.Empty)
            {
                projects = await _dbContext.Projects.Where(p => p.Description.Contains(query)).ToListAsync();
                return projects;
            }

            projects = await _dbContext.Projects.ToListAsync();

            return projects;
        }

        public async Task<Project?> GetByIdAsync(int projectId)
        {
            var project = await _dbContext.Projects
                .Include(p => p.Client)
                .Include(p => p.Freelancer)
                .SingleOrDefaultAsync(p => p.Id == projectId);

            return project;
        }

        public async Task AddAsync(Project project)
        {
            await _dbContext.Projects.AddAsync(project);
            await _dbContext.SaveChangesAsync();
        }

        public async Task SaveChangesAsync() => await _dbContext.SaveChangesAsync();
    }
}
