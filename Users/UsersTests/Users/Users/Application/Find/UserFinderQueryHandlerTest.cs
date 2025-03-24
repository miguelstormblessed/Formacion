/*using FluentAssertions;
using Users.Shared.Users.Domain.Querys;
using Users.Shared.Users.Domain.Responses;
using UsersManagement.Users.Application.Find;
using UsersManagement.Users.Domain;
using UsersTests.Users.Domain;

namespace UsersTests.Users.Application.Find;


public class UserFinderQueryHandlerTest : BookingsModuleApplicationTestCase
{
    private readonly UserFinderQueryHandler _handler;

    public UserFinderQueryHandlerTest()
    {
        UserFinder _userFinder = new UserFinder(this.UserRepository.Object);
        _handler = new UserFinderQueryHandler(_userFinder);
    }
    [Fact]
    public async Task ShouldReturnUserResponse()
    {
        // GIVEN
        Usuario user = UserMother.CreateRandom();
        this.ShouldFindUser(user.Id, user);
        UserFinderQuery query = UserFinderQuery.Create(user.Id.Id);
        // WHEN
        UserResponse result = await this._handler.HandleAsync(query);
        // THEN
        result.Id.Should().Be(user.Id.Id);
        result.Email.Should().Be(user.Email.Email);
        result.Name.Should().Be(user.Name.Name);
        result.State.Should().Be(user.State.Active);
    }
}*/