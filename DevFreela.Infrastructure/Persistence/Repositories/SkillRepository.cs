using Dapper;
using DevFreela.Core.DTOs;
using DevFreela.Core.Repositories.Interfaces;
using DevFreela.Infrastructure.Factories;
using Microsoft.Extensions.Configuration;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class SkillRepository : ISkillRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ConnectionFactory _connectionFactory;

        public SkillRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionFactory = new ConnectionFactory(configuration);
        }

        public async Task<List<SkillDTO>> GetAllAsync()
        {
            using (var connection = _connectionFactory.GetConnectionDevFreela())
            {
                var sql = @"SELECT 
                                Id, 
                                Description 
                            FROM Skills ";

                var result = await connection.QueryAsync<SkillDTO>(sql);

                return result.ToList();
            }

            // Below we have the query using EF CORE

            //var skills = _dbContext.Skills;

            //var skillsViewModel = skills.Select(s => new SkillViewModel(s.Id, s.Description)).ToList();
        }

        public Task<SkillDTO> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
