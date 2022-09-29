using DevFreela.Core.Entities;

namespace DevFreela.Core.Repositories.Interfaces
{
    public interface IProjectRepository
    {
        Task<List<Project>> GetAllAsync(string query);
        Task<Project?> GetByIdAsync(int projectId);
        Task AddAsync(Project project);
        Task SaveChangesAsync();
    }
}
