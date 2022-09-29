using DevFreela.Core.DTOs;

namespace DevFreela.Core.Repositories.Interfaces
{
    public interface ISkillRepository
    {
        Task<List<SkillDTO>> GetAllAsync();

        Task<SkillDTO> GetByIdAsync(int id);
    }
}
