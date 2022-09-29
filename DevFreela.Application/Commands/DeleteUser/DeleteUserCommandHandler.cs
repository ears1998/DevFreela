using DevFreela.Core.Entities;
using DevFreela.Core.Repositories.Interfaces;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Unit>
    {

        private readonly IUserRepository _userRepository;

        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {

            var user = await _userRepository.GetByIdAsync(request.Id);

            if (user != null) user.Inactivate();

            await _userRepository.SaveChangesAsync();

            return Unit.Value;

        }
    }
}
