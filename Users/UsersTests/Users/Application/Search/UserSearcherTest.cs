using FluentAssertions;
using Moq;
using Users.Users.Application.Search;
using Users.Users.Domain;
using UsersTests.Users.Domain;

namespace UsersTests.Users.Application.Search;

public class UserSearcherTest : UserModuleApplicationUnitTestCase
{
    private readonly UserSearcher _userSearcher;

    public UserSearcherTest()
    {
        _userSearcher = new UserSearcher(this.UserRepository.Object);
        
    }
    [Fact]
    public async Task ShouldCallSearchUsersOnce()
    {
        // GIVEN
        // WHEN
        var result = await this._userSearcher.ExecuteAsync();
        // THEN
        this.UserRepository.Verify(x => x.SearchUsers(), Times.Once);
    }
    [Fact]
    public async Task ShouldReturnListOfUsers_WhenSearchUsersIsCalled()
    {
        // GIVEN
        List<Usuario> usersList = new List<Usuario>
        {
            UserMother.CreateRandom(),
            UserMother.CreateRandom(),
        };

        this.ShouldSearchUsers(usersList);
        // WHEN
        var result = await this._userSearcher.ExecuteAsync();
        
        // THEN
        
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(usersList);
    }

    [Fact]
    public async Task ShouldReturnEmptyList_WhenSearchUsersIsCalled()
    {
        // GIVEN
        List<Usuario> usersList = new List<Usuario>();
        this.ShouldSearchUsers(usersList);
        // WHEN
        var result = await this._userSearcher.ExecuteAsync();
        // THEN
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task ShouldReturnCorrectTypeOfEnumerable()
    {
        // GIVEN
        List<Usuario> usersList = new List<Usuario>();
        this.ShouldSearchUsers(usersList);
        // WHEN
        var result = await this._userSearcher.ExecuteAsync();
        // THEN
        result.Should().BeOfType(typeof(List<Usuario>));

    }
}