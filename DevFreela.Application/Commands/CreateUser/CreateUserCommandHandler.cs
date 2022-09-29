using DevFreela.Core.Entities;
using DevFreela.Core.Repositories.Interfaces;
using DevFreela.Core.Services;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
    {

        private readonly IUserRepository _userRepository;
        private readonly IAuthorizationService _authorizationService;

        public CreateUserCommandHandler(IUserRepository userRepository, IAuthorizationService authorizationService)
        {
            _userRepository = userRepository;
            _authorizationService = authorizationService;
        }

        public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var hashedPassword = _authorizationService.ComputeSha256Hash(request.Password);

            var newUser = new User(request.FullName, request.Email, request.BirthDate, hashedPassword, request.Role);

            await _userRepository.AddAsync(newUser);

            return newUser;

        }
    }
}
