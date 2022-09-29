using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<User>
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
