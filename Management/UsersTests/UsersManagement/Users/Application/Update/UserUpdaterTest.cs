using Cojali.Shared.Domain.Bus.Event;
using Cojali.Shared.Infrastructure.Bus.Database;
using Cojali.Shared.Infrastructure.Bus.Memory;
using FluentAssertions;
using Moq;
using UsersManagement.Senders.Application;
using UsersManagement.Senders.Domain;
using UsersManagement.Senders.Domain.ValueObject;
using UsersManagement.Shared.Users.Domain.DomainEvents;
using UsersManagement.Users.Application.Find;
using UsersManagement.Users.Application.Update;
using UsersManagement.Users.Domain;
using UsersManagement.Users.Domain.ValueObject;
using UsersTests.UsersManagement.Users.Domain;
using UsersTests.UsersManagement.Users.Domain.ValueObject;

namespace UsersTests.UsersManagement.Users.Application.Update;

public class UserUpdaterTest : UserModuleApplicationUnitTestCase
{
    private readonly UserUpdater _userUpdater;
    private readonly UserFinder _userFinder;
    public UserUpdaterTest()
    {
        this._userFinder = new UserFinder(this.UserRepository.Object);
        this._userUpdater = new UserUpdater(this.UserRepository.Object, this._userFinder,this.EventBus.Object, this.QueryBus.Object);
    }
    [Fact]
    public void ShouldCallUserUpdateOnce()
    {
        // GIVEN
        Usuario user = UserMother.CreateRandom();
        this.ShouldFindUser(user.Id, user);
        // WHEN
        this._userUpdater.Execute(user.Id, user.Name, user.Email, user.State, Guid.NewGuid().ToString());
        // THEN
        this.ShouldHaveCalledUpdateWithCorrectParametersOnce(user);

    }

    [Fact]
    public void ShouldCallPublishOnceWithCorrectParameters()
    {
        // GIVEN
        Usuario user = UserMother.CreateRandom();
        this.ShouldFindUser(user.Id, user);
        
        // WHEN
        this._userUpdater.Execute(user.Id, user.Name, user.Email, user.State, Guid.NewGuid().ToString());
        // THEN
        this.ShouldHaveCalledPublishWithcorrectParametersOnce<UserUpdated>();
    }
   
}