using Moq;
using UsersManagement.Senders.Application;
using UsersManagement.Senders.Domain;
using UsersManagement.Senders.Domain.ValueObject;
using UsersManagement.Shared.Users.Domain.DomainEvents;
using UsersManagement.Users.Application.Create;
using UsersManagement.Users.Domain;
using UsersManagement.Vehicles.Application.Find;
using UsersManagement.Vehicles.Domain;
using UsersManagement.Vehicles.Domain.ValueObject;
using UsersTests.UsersManagement.Users.Domain;
using UsersTests.UsersManagement.Vehicles.Domain;

namespace UsersTests.UsersManagement.Users.Application.Create;

public class UserInactiveCreatorTest : UserModuleApplicationUnitTestCase
{
    private readonly UserInactiveCreator _userInactiveCreator;
    public UserInactiveCreatorTest()
    {
        this._userInactiveCreator = new UserInactiveCreator(this.UserRepository.Object, this.EventBus.Object, this.QueryBus.Object, this.CommandBus.Object);
    }
    [Fact]
    public void UserInactiveCreator_ShouldCallSaveWithCorrectParametersOnce()
    {
        // Given
        Usuario user = UserMother.CreateRandom();
        user.State.Active = false;
        Vehicle vehicle = VehicleMother.CreateRandom();
        this.ShouldSaveUsers(user);
        this.ShouldFindVehicle(vehicle.Id,vehicle);
        // When
        this._userInactiveCreator.Execute(user.Id, user.Name, user.Email, vehicle.Id.IdValue);
        // Then 
        this.ShouldHaveCalledSaveWithCorrectParametersOnce(user);
    }
    [Fact]
    public void ShouldCallPublishOnce()
    {
        // GIVEN
        Usuario user = UserMother.CreateRandom();
        Vehicle vehicle = VehicleMother.CreateRandom();
        this.ShouldSaveUsers(user);
        this.ShouldFindVehicle(vehicle.Id,vehicle);
        // WHEN
        this._userInactiveCreator.Execute(user.Id, user.Name, user.Email,vehicle.Id.IdValue);
        // THEN
        this.ShouldHaveCalledPublishWithcorrectParametersOnce<InactiveUserCreated>();
    }
}