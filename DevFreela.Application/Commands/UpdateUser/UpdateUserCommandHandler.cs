using DevFreela.Core.Repositories.Interfaces;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Unit>
    {

        private readonly IUserRepository _userRepository;

        public UpdateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {

            var user = await _userRepository.GetByIdAsync(request.Id);

            if (user != null) user.Update(request.Email);

            await _userRepository.SaveChangesAsync();

            return Unit.Value;

        }
    }
}
