using DevFreela.Application.ViewModels;
using MediatR;

namespace DevFreela.Application.Queries.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<List<UserViewModel>>
    {
    }
}
