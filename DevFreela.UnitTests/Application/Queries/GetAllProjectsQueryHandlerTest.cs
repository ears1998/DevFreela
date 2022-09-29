using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories.Interfaces;
using Moq;
using Xunit;

namespace DevFreela.UnitTests.Application.Queries
{
    public class GetAllProjectsQueryHandlerTest
    {
        [Fact]
        public async Task ThreeProjectsExistsInDb_Executed_ReturnThreeProjectsViewModel()
        {
            //Arrange -------------------------------------------------------------------------------------
            var projects = new List<Project>()
            {
                new Project("Test 1","Description 1", 1, 2, 10000),
                new Project("Test 2","Description 2", 1, 2, 20000),
                new Project("Test 3","Description 3", 1, 2, 30000),
            };

            var projectsRepositoryMock = new Mock<IProjectRepository>();
            projectsRepositoryMock.Setup(p => p.GetAllAsync("").Result).Returns(projects);

            var getAllProjectsQuery = new GetAllProjectsQuery("");
            var getAllProjectsQueryHandler = new GetAllProjectsQueryHandler(projectsRepositoryMock.Object);

            //Act -------------------------------------------------------------------------------------
            var projectsListViewModel = await getAllProjectsQueryHandler.Handle(getAllProjectsQuery, new CancellationToken());

            //Assert -------------------------------------------------------------------------------------
            Assert.NotNull(projectsListViewModel);
            Assert.NotEmpty(projectsListViewModel);
            Assert.Equal(projectsListViewModel.Count, projects.Count);

            projectsRepositoryMock.Verify(p => p.GetAllAsync("").Result, Times.Once);
        }
    }
}
