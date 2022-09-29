using MediatR;

namespace DevFreela.Application.Commands.CreateComment
{
    public class CreateCommentCommand : IRequest<Unit>
    {
        public string Content { get; set; }
        public int ProjectId { get; private set; }
        public int UserId { get; set; }

        public void SetId(int projectId) => ProjectId = projectId;
    }
}
