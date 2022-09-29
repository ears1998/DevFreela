using DevFreela.Application.Queries.GetAllUsers;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories.Interfaces;
using Moq;
using Xunit;

namespace DevFreela.UnitTests.Application.Queries
{
    public class GetAllUsersQueryHandlerTest
    {
        [Fact]
        public async Task ThreeUsersExistsInDatabase_Executed_ReturnThreeUsers()
        {
            //Arrange

            var userList = new List<User>()
            {
                new User("User1", "email@1", new DateTime(), "password1", "Client"),
                new User("User2", "email@2", new DateTime(), "password2", "Freelancer"),
                new User("User3", "email@3", new DateTime(), "password3", "Client"),
            };

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(u => u.GetAllAsync().Result).Returns(userList);

            var getAllUsersQuery = new GetAllUsersQuery();
            var getAllUsersQueryHandler = new GetAllUsersQueryHandler(userRepositoryMock.Object);

            //Act
            var userViewModelList = await getAllUsersQueryHandler.Handle(getAllUsersQuery, new CancellationToken());

            //Assert
            Assert.NotNull(userViewModelList);
            Assert.NotEmpty(userViewModelList);
            Assert.Equal(userViewModelList.Count, userList.Count);
            userRepositoryMock.Verify(u => u.GetAllAsync().Result, Times.Once);

        }
    }
}
