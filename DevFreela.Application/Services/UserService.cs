using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;

namespace DevFreela.Application.Services
{
    public class UserService : IUserService
    {
        private readonly DevFreelaDbContext _dbContext;

        public UserService(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    
        public int Create(CreateUserInputModel inputModel)
        {
            var newUser = new User(inputModel.FullName, inputModel.Email, inputModel.BirthDate);

            _dbContext.Users.Add(newUser);

            return newUser.Id;
        }

        public void Delete(int userId)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);

            if (user != null) user.Inactivate();
        }

        public List<UserViewModel> GetAll()
        {
            var users = _dbContext.Users;

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

        public UserViewModel GetById(int userId)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);

            var userViewModel = new UserViewModel(
                                                  user.Id,
                                                  user.FullName,
                                                  user.Email,
                                                  user.Status,
                                                  user.Skills,
                                                  user.OwnedProjects,
                                                  user.FreelanceProjects
                                                 );

            return userViewModel;
        }

        public void Update(UpdateUserInputModel inputModel)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == inputModel.Id);

            if (user != null) user.Update(inputModel.Email);
        }
    }
}
