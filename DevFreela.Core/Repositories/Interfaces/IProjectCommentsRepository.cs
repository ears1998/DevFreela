using DevFreela.Core.Entities;

namespace DevFreela.Core.Repositories.Interfaces
{
    public interface IProjectCommentsRepository
    {
        Task AddAsync(ProjectComment comment);
    }
}
