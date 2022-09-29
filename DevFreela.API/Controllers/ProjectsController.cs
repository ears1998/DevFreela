using DevFreela.Application.Commands.CreateComment;
using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Application.Commands.FinishProject;
using DevFreela.Application.Commands.StartProject;
using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Application.Queries.GetProjectById;
using DevFreela.Core.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/projects")]
    public class ProjectsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // api/projects?query=net core
        [HttpGet]
        [Authorize(Roles = $"{Roles.Client}, {Roles.Freelancer}")]
        public async Task<IActionResult> Get(string? query)
        {
            var getAllProjectsQuery = new GetAllProjectsQuery(query);

            var projects = await _mediator.Send(getAllProjectsQuery);

            return Ok(projects);
        }

        // api/projects/1
        [HttpGet("{projectId}")]
        [Authorize(Roles = $"{Roles.Client}, {Roles.Freelancer}")]
        public async Task<IActionResult> GetById(int projectId)
        {
            var getProjectByIdQuery = new GetProjectByIdQuery(projectId);

            var project = await _mediator.Send(getProjectByIdQuery);

            return Ok(project);
        }

        [HttpPost]
        [Authorize(Roles = $"{Roles.Client}")]
        public async Task<IActionResult> Post([FromBody] CreateProjectCommand createProjectCommand)
        {
            if (createProjectCommand.Title.Length > 50) return BadRequest();

            var idCreatedProject = await _mediator.Send(createProjectCommand);

            return CreatedAtAction(nameof(GetById), new { projectId = idCreatedProject}, createProjectCommand);
        }

        // api/projects/2
        [HttpPut("{projectId}")]
        [Authorize(Roles = $"{Roles.Client}")]
        public async Task<IActionResult> Put([FromBody] UpdateProjectCommand updateProjectCommand, int projectId)
        {
            updateProjectCommand.SetId(projectId);

            if (updateProjectCommand.Description.Length > 200) return BadRequest();

            await _mediator.Send(updateProjectCommand);

            return NoContent();
        }

        // api/projects/3
        [HttpDelete("{projectId}")]
        [Authorize(Roles = $"{Roles.Client}")]
        public async Task<IActionResult> Delete(int projectId)
        {
            var deleteProjectCommand = new DeleteProjectCommand(projectId);

            await _mediator.Send(deleteProjectCommand);

            return NoContent();
        }

        [HttpPost("{projectId}/comments")]
        [Authorize(Roles = $"{Roles.Client}, {Roles.Freelancer}")]
        public async Task<IActionResult> PostComment([FromBody] CreateCommentCommand createCommentCommand, int projectId)
        {
            createCommentCommand.SetId(projectId);
            await _mediator.Send(createCommentCommand);
            return NoContent();
        }

        [HttpPut("{projectId}/start")]
        [Authorize(Roles = $"{Roles.Client}")]
        public async Task<IActionResult>  Start(int projectId)
        {
            var startProjectCommand = new StartProjectCommand(projectId);

            await _mediator.Send(startProjectCommand);

            return NoContent();
        }

        [HttpPut("{projectId}/finish")]
        [Authorize(Roles = $"{Roles.Client}")]
        public async Task<IActionResult> Finish(int projectId)
        {
            var finishProjectCommand = new FinishProjectCommand(projectId);

            await _mediator.Send(finishProjectCommand);

            return NoContent();
        }
    }
}
