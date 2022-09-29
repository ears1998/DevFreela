using DevFreela.Application.ViewModels;
using DevFreela.Core.Repositories.Interfaces;
using MediatR;

namespace DevFreela.Application.Queries.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserViewModel>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserViewModel>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllAsync();

            var usersViewModel = users.Select(u => new UserViewModel(
                                                            u.Id, 
                                                            u.FullName, 
                                                            u.Email, 
                                                            u.Status, 
                                                            u.Skills, 
                                                            u.OwnedProjects, 
                                                            u.FreelanceProjects
                                                        )).ToList();

            return usersViewModel;
        }
    }
}
