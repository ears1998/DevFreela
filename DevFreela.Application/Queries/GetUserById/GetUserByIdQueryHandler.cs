using DevFreela.Application.ViewModels;
using DevFreela.Core.Repositories.Interfaces;
using MediatR;

namespace DevFreela.Application.Queries.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserViewModel>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserViewModel> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id);

            if (user == null) return null;

            var userViewModel = new UserViewModel(
                                        user.Id, 
                                        user.FullName, 
                                        user.Email, 
                                        user.Status, 
                                        user.Skills, 
                                        user.OwnedProjects, 
                                        user.FreelanceProjects);

            return userViewModel;

        }
    }
}
