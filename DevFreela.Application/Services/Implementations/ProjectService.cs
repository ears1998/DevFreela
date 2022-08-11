﻿using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;

namespace DevFreela.Application.Services.Implementations
{
    public class ProjectService : IProjectService
    {
        private readonly DevFreelaDbContext _dbContext;

        public ProjectService(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Create(NewProjectInputModel inputModel)
        {
            var project = new Project(inputModel.Title, inputModel.Description, inputModel.ClientId, inputModel.FreelancerId, inputModel.TotalCost);

            _dbContext.Projects.Add(project);

            return project.Id;
        }

        public void CreateComment(CreateCommentInputModel inputModel)
        {
            var comment = new ProjectComment(inputModel.Content, inputModel.ProjectId, inputModel.UserId);

            _dbContext.ProjectComments.Add(comment);
        }

        public void Delete(int projectId)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == projectId);

            if (project != null) project.Cancel();
        }

        public void Finish(int projectId)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == projectId);

            if(project != null) project.Finish();
        }

        public List<ProjectViewModel> GetAll(string query)
        {
            var projects = _dbContext.Projects;

            var projectsViewModel = projects.Select(p => new ProjectViewModel(p.Title, p.CreatedAt)).ToList();

            return projectsViewModel;
        }

        public ProjectDetailsViewModel GetById(int projectId)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == projectId);

            if (project == null) return null;

            var projectDetailsViewModel = new ProjectDetailsViewModel
                                            (
                                            project.Id,
                                            project.Title,
                                            project.Description,
                                            project.TotalCost,
                                            project.StartedAt,
                                            project.FinishedAt
                                            );

            return projectDetailsViewModel;
        }

        public void Start(int projectId)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == projectId);

            if (project != null) project.Start();
        }

        public void Update(UpdateProjectInputModel updateProjectModel)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == updateProjectModel.Id);

            if (project != null) project.Update(updateProjectModel.Title, updateProjectModel.Description, updateProjectModel.TotalCost);
        }
    }
}
