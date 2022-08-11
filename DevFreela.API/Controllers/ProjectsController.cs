using DevFreela.API.Models;
using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DevFreela.API.Controllers
{
    [Route("api/projects")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }
        // api/projects?query=net core
        [HttpGet]
        public IActionResult Get(string query)
        {
            var projects = _projectService.GetAll(query);

            return Ok(projects);
        }

        // api/projects/1
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var project = _projectService.GetById(id);

            if (project == null) return NotFound();

            return Ok(project);
        }

        [HttpPost]
        public IActionResult Post([FromBody] NewProjectInputModel inputModel)
        {
            if (inputModel.Title.Length > 50) return BadRequest();

            var idCreatedProject = _projectService.Create(inputModel);

            return CreatedAtAction(nameof(GetById), new { id = idCreatedProject}, inputModel);
        }

        // api/projects/2
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] UpdateProjectInputModel updateProjectModel)
        {
            if (updateProjectModel.Description.Length > 200) return BadRequest();

            _projectService.Update(updateProjectModel);

            return NoContent();
        }

        // api/projects/3
        [HttpDelete("{id}")]
        public IActionResult Delete(int projectId)
        {
            _projectService.Delete(projectId);
            return NoContent();
        }

        [HttpPost("{id}/comments")]
        public IActionResult PostComment([FromBody] CreateCommentInputModel createCommentModel)
        {
            _projectService.CreateComment(createCommentModel);
            return NoContent();
        }

        [HttpPut("{id}/start")]
        public IActionResult Start(int projectId)
        {
            _projectService.Start(projectId);
            return NoContent();
        }

        [HttpPut("{id}/finish")]
        public IActionResult Finish(int projectId)
        {
            _projectService.Finish(projectId);
            return NoContent();
        }
    }
}
