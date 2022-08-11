using DevFreela.Application.InputModels;
using DevFreela.Application.ViewModels;

namespace DevFreela.Application.Services.Interfaces
{
    public interface IProjectService
    {

        List<ProjectViewModel> GetAll(string query);
        ProjectDetailsViewModel GetById(int projectId);
        int Create(NewProjectInputModel newProjectInputModel);
        void Update(UpdateProjectInputModel updateProjectModel);
        void Delete(int projectId);
        void CreateComment(CreateCommentInputModel createCommentInputModel);
        void Start(int projectId);
        void Finish(int projectId);

    }
}
