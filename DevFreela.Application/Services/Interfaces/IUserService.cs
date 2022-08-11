using DevFreela.Application.InputModels;
using DevFreela.Application.ViewModels;

namespace DevFreela.Application.Services.Interfaces
{
    public interface IUserService
    {
        List<UserViewModel> GetAll();
        UserViewModel GetById(int userId);
        int Create(CreateUserInputModel inputModel);
        void Update(UpdateUserInputModel inputModel);
        void Delete(int userId);
    }
}
