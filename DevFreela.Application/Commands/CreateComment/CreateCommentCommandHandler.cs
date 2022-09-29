using DevFreela.Core.Entities;
using DevFreela.Core.Repositories.Interfaces;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.CreateComment
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, Unit>
    {
        private readonly IProjectCommentsRepository _projectCommentsRepository;

        public CreateCommentCommandHandler(IProjectCommentsRepository projectCommentsRepository)
        {
            _projectCommentsRepository = projectCommentsRepository;
        }

        public async Task<Unit> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = new ProjectComment(request.Content, request.ProjectId, request.UserId);

            await _projectCommentsRepository.AddAsync(comment);

            return Unit.Value;

        }
    }
}
