using DevFreela.Core.Entities;
using DevFreela.Core.Enums;
using Xunit;

namespace DevFreela.UnitTests.Core.Entities
{
    public class ProjectTests
    {
        [Fact]
        public void TestIfStartProjectWorks()
        {
            var project = new Project("Test Project", "Test project description", 1, 2, 10000);

            project.Start();

            Assert.Equal(ProjectStatus.InProgress, project.Status);
            Assert.NotNull(project.StartedAt);
        }
    }
}
