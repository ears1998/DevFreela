using MediatR;

namespace DevFreela.Application.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<Unit>
    {
        public int Id { get; private set; }
        public string Email { get; private set; }
    }
}
