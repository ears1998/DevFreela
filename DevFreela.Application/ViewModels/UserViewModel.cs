using DevFreela.Core.Entities;
using DevFreela.Core.Enums;

namespace DevFreela.Application.ViewModels
{
    public class UserViewModel
    {
        public UserViewModel(int id, string fullName, string email, UserStatus status, List<UserSkill> skills, List<Project> ownedProjects, List<Project> freelanceProjects)
        {
            Id = id;
            FullName = fullName;
            Email = email;
            Status = status;
            Skills = skills;
            OwnedProjects = ownedProjects;
            FreelanceProjects = freelanceProjects;
        }

        public int Id { get; private set; }
        public string FullName { get; private set; }
        public string Email { get; private set; }
        public UserStatus Status { get; private set; }
        public List<UserSkill> Skills { get; private set; }
        public List<Project> OwnedProjects { get; private set; }
        public List<Project> FreelanceProjects { get; private set; }
    }
}
