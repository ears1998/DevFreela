using DevFreela.API.Models;
using DevFreela.Application.Commands.CreateUser;
using DevFreela.Application.Commands.DeleteUser;
using DevFreela.Application.Commands.LoginUser;
using DevFreela.Application.Queries.GetAllUsers;
using DevFreela.Application.Queries.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/users")]
    //[ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var getAllUsersQuery = new GetAllUsersQuery();

            var user = await _mediator.Send(getAllUsersQuery);

            return Ok(user);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetById(int userId)
        {
            var getUserByIdQuery = new GetUserByIdQuery(userId);

            var user = await _mediator.Send(getUserByIdQuery);

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserCommand createUserCommand)
        {

            var createdUser = await _mediator.Send(createUserCommand);

            return CreatedAtAction(nameof(GetById), new { userId = createdUser.Id }, createUserCommand);

        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete(int userId)
        {

            var deleteUserCommand = new DeleteUserCommand(userId);

            await _mediator.Send(deleteUserCommand);

            return NoContent();

        }

        // [api/Users/login]
        [HttpPut("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand loginUserCommand)
        {
            var userViewModel = await _mediator.Send(loginUserCommand);

            if (userViewModel == null) return BadRequest();

            return Ok(userViewModel);
        }
    }
}
