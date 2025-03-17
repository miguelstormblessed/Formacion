using FluentAssertions;
using Moq;
using Org.BouncyCastle.Tls.Crypto;
using UsersManagement.Shared.Users.Domain.Exceptions;
using UsersManagement.Users.Application.Find;
using UsersManagement.Users.Domain;
using UsersTests.UsersManagement.Users.Domain;

namespace UsersTests.UsersManagement.Users.Application.Find;

public class UserFinderTest : UserModuleApplicationUnitTestCase
{
   private readonly UserFinder _userFinder;
    public UserFinderTest()
    {
        this._userFinder = new UserFinder(this.UserRepository.Object);
    }
    
    [Fact]
    public void ShouldCallFinderOnce_WithCorrectValues()
    {
        // GIVEN
        Usuario user = UserMother.CreateRandom();
        this.ShouldFindUser(user.Id, user); // Setup
        // WHEN
        Usuario result = _userFinder.Execute(user.Id);
        // THEN
        this.ShouldHaveCalledFinderWithCorrectParametersOnce(user.Id);
    }

    [Fact]
    public void ShouldReturnEquivalentUser()
    {
        // Given
        Usuario user = UserMother.CreateRandom();
        this.ShouldFindUser(user.Id, user);
        // When
        Usuario result = _userFinder.Execute(user.Id);
        // Then
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(user);
    }

    [Fact]
    public void ShouldReturnException_WhenUserNotFound()
    {
        // GIVEN
        Usuario user = UserMother.CreateRandom();
        // WHEN
        var action = () => _userFinder.Execute(user.Id);
        // THEN
        action.Should().Throw<UserNotFoundException>();
    }

    [Fact]
    public void ShouldReturnCorrectType()
    {
        // GIVEN
        Usuario user = UserMother.CreateRandom();
        this.ShouldFindUser(user.Id, user);
        // WHEN
        Usuario result = _userFinder.Execute(user.Id);
        // THEN
        result.Should().NotBeNull();
        result.Should().BeOfType<Usuario>();
    }
    
}