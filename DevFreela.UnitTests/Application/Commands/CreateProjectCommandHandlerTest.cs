using DevFreela.Application.Commands.CreateProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories.Interfaces;
using Moq;
using Xunit;

namespace DevFreela.UnitTests.Application.Commands
{
    public class CreateProjectCommandHandlerTest
    {
        [Fact]
        public async Task InputDataIsOk_Executed_ReturnProjectId()
        {
            //Arrange -------------------------------------------------------------------------------------
            var projectRepositoryMock = new Mock<IProjectRepository>();

            var createProjectCommandHandler = new CreateProjectCommandHandler(projectRepositoryMock.Object);
            var createProjectCommand = new CreateProjectCommand()
            {
                Title = "Test Title",
                Description = "Test Description",
                TotalCost = 10000,
                FreelancerId = 1,
                ClientId = 2
            };

            //Act -------------------------------------------------------------------------------------
            var projectId = await createProjectCommandHandler.Handle(createProjectCommand, new CancellationToken());

            //Assert -------------------------------------------------------------------------------------
            Assert.True(projectId >= 0);
            projectRepositoryMock.Verify(p => p.AddAsync(It.IsAny<Project>()), Times.Once);
        }

    }
}
