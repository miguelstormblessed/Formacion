using Moq;
using UsersManagement.Shared.Users.Domain.DomainEvents;
using UsersManagement.Shared.Vehicles.Domain.Commands;
using UsersManagement.Shared.Vehicles.Domain.Exceptions;
using UsersManagement.Shared.Vehicles.Domain.Querys;
using UsersManagement.Shared.Vehicles.Domain.Responses;
using UsersManagement.Users.Application.Create;
using UsersManagement.Users.Domain;
using UsersManagement.Vehicles.Application.Find;
using UsersManagement.Vehicles.Domain;
using UsersManagement.Vehicles.Domain.ValueObject;
using UsersTests.UsersManagement.Users.Domain;
using UsersTests.UsersManagement.Vehicles.Domain;
using UsersTests.UsersManagement.Vehicles.Domain.ValueObject;

namespace UsersTests.UsersManagement.Users.Application.Create;

public class UserActiveCreatorTest : UserModuleApplicationUnitTestCase
{
    private readonly UserActiveCreator _userActiveCreator;
    public UserActiveCreatorTest()
    {
        this._userActiveCreator = new UserActiveCreator(this.UserRepository.Object, this.EventBus.Object, this.QueryBus.Object, this.CommandBus.Object);
    }
    [Fact]
    public void UserActiveCreator_ShouldCallSaveWithCorrectParametersOnce()
    {
        // Given
        
        Usuario user = UserMother.CreateRandom();
        Vehicle vehicle = VehicleMother.CreateRandom();
        this.ShouldSaveUsers(user);
        this.ShouldFindVehicle(vehicle.Id,vehicle);
        // When
        this._userActiveCreator.Execute(user.Id, user.Name, user.Email, vehicle.Id.IdValue);
        // Then 
        this.ShouldHaveCalledSaveWithCorrectParametersOnce(user);
    }
    [Fact]
    public void ShouldHavePublishOnce()
    {
        // GIVEN
        Usuario user = UserMother.CreateRandom();
        Vehicle vehicle = VehicleMother.CreateRandom();
        this.ShouldSaveUsers(user);
        this.ShouldFindVehicle(vehicle.Id,vehicle);
        // WHEN
        this._userActiveCreator.Execute(user.Id, user.Name, user.Email, vehicle.Id.IdValue);
        // THEN
        this.ShouldHaveCalledPublishWithcorrectParametersOnce<ActivatedUserCreated>();
    }

    [Fact]
    public async Task ShouldCallAskAsyncWithCorrectParametersOnce()
    {
        // GIVEN
        VehicleFinderQuery query = VehicleFinderQuery.Create(Guid.NewGuid().ToString());
        Usuario user = UserMother.CreateRandom();
        // WHEN
        await this._userActiveCreator.Execute(
            user.Id, user.Name, user.Email, query.VehicleId
        );
        // THEN
        this.ShouldHaveCalledAskAsyncWithCorrectParametersOnce(query);
    }

    [Fact]
    public async Task ShouldCallDistpatchAsyncWithCorrectParametersOnce()
    {
        // GIVEN
        VehicleCreatorCommand command = VehicleCreatorCommand.Create(Guid.NewGuid().ToString());
        VehicleFinderQuery query = VehicleFinderQuery.Create(Guid.NewGuid().ToString());
        Usuario user = UserMother.CreateRandom();
        VehicleResponse vehicleResponse = VehicleResponse.Create(
            command.VehicleId,
            command.VehicleRegistrationNumber,
            command.VehicleColor
            );

        this.ShouldThrowVehicleNotFoundExceptionInFirstCallAndReturnsVehicleInSecondCall(vehicleResponse);
        
        // WHEN
        await this._userActiveCreator.Execute(
            user.Id, user.Name, user.Email, command.VehicleId
        );
        // THEN
        this.ShouldHaveCalledDispatchAsyncWithCorrectParametersOnce(command);
    }
    
}