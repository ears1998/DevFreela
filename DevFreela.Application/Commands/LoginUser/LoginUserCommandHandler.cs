using DevFreela.Application.ViewModels;
using DevFreela.Core.Repositories.Interfaces;
using DevFreela.Core.Services;
using MediatR;

namespace DevFreela.Application.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserViewModel>
    {

        private readonly IAuthorizationService _authorizationService;
        private readonly IUserRepository _userRepository;

        public LoginUserCommandHandler(IAuthorizationService authorizationService, IUserRepository userRepository)
        {
            _authorizationService = authorizationService;
            _userRepository = userRepository;
        }

        public async Task<LoginUserViewModel> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var hashedPassword = _authorizationService.ComputeSha256Hash(request.Password);

            var user = await _userRepository.GetUserByEmailAndPasswordAsync(request.Email, hashedPassword);

            if (user == null) return null;

            var userToken = _authorizationService.GenerateJwtToken(user.Email, user.Role);

            var loginUserViewModel = new LoginUserViewModel(user.Email, userToken);

            return loginUserViewModel;
        }
    }
}
