using DevFreela.Core.Entities;
using DevFreela.Core.Repositories.Interfaces;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.DeleteProject
{
    public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, Unit>
    {

        private readonly IProjectRepository _projectRepository;

        public DeleteProjectCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<Unit> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {

            var project = await _projectRepository.GetByIdAsync(request.Id);

            if (project != null) project.Cancel();

            await _projectRepository.SaveChangesAsync();

            return Unit.Value;

        }
    }
}
