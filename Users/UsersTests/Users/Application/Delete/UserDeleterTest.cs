using FluentAssertions;
using Users.Shared.Users.Domain.DomainEvents;
using Users.Shared.Users.Domain.Exceptions;
using Users.Shared.Vehicles.Domain.Responses;
using UsersManagement.Users.Application.Delete;
using UsersManagement.Users.Application.Find;
using UsersManagement.Users.Domain;
using UsersTests.Shared.Vehicles.Domain.Responses;
using UsersTests.Users.Domain;
using UsersTests.Users.Domain.ValueObject;

namespace UsersTests.Users.Application.Delete;

public class UserDeleterTest : UserModuleApplicationUnitTestCase
{
    private readonly UserDeleter _userDeleter;
    private readonly UserFinder _userFinder;
    public UserDeleterTest()
    {
        this._userFinder = new UserFinder(this.UserRepository.Object);
        this._userDeleter = new UserDeleter(this.UserRepository.Object, this._userFinder, this.EventBus.Object);
    }

    [Fact]
    public void ShouldCallDeleterOnce()
    {
        // GIVEN
        Usuario user = UserMother.CreateRandom();
        this.ShouldFindUser(user.Id, user);
        // THEN
        this._userDeleter.Execute(user.Id);
        // WHEN
        this.UserRepository.Verify(x => x.Delete(user.Id));
    }
    [Fact]
    public void ShouldReturnException_WhenUserIsInactive()
    {
        // GIVEN
        Usuario user = Usuario.CreateUserDeactivated(
            UserIdMother.CreateRandom(),
            UserNameMother.CreateRandom(),
            UserEmailMother.CreateRandom(),
            VehicleResponseMother.CreateRandom());
        // We need this because Deleter uses the finder
        this.ShouldFindUser(user.Id, user);
        // WHEN
        var action = () =>this._userDeleter.Execute(user.Id);
        // THEN
        action.Should().Throw<UserAlreadyDeletedException>();
    }

    [Fact]
    public void ShouldDeleteAnUser()
    {
        // GIVEN
        Usuario user = Usuario.CreateUserActivated(
            UserIdMother.CreateRandom(),
            UserNameMother.CreateRandom(),
            UserEmailMother.CreateRandom(),
            VehicleResponseMother.CreateRandom()
        );
        this.ShouldFindUser(user.Id, user);
        // WHEN
        user.Delete(user.Id, user.Name, user.Email);
        // THEN
        user.State.Active.Should().BeFalse();
    }
    [Fact]
    public void ShouldCallPublishOnce()
    {
        // GIVEN
        Usuario user = UserMother.CreateRandom();
        this.ShouldFindUser(user.Id, user);
        // WHEN
        this._userDeleter.Execute(user.Id);
        // THEN
        this.ShouldHaveCalledPublishWithcorrectParametersOnce<UserDeleted>();
    }
    
}