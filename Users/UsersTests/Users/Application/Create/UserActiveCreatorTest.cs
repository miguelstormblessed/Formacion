using System.Net;
using Newtonsoft.Json;
using Users.Shared.Vehicles.Domain.Responses;
using UsersManagement.Users.Application.Create;
using UsersManagement.Users.Domain;
using UsersTests.Shared.Vehicles.Domain.Responses;
using UsersTests.Users.Domain;

namespace UsersTests.Users.Application.Create;

public class UserActiveCreatorTest : UserModuleApplicationUnitTestCase
{
    private readonly UserActiveCreator _userActiveCreator;
    public UserActiveCreatorTest()
    {
        this._userActiveCreator = new UserActiveCreator(this.UserRepository.Object, this.EventBus.Object, this.QueryBus.Object, this.CommandBus.Object, this.HttpClientService.Object);
    }
    [Fact]
    public void UserActiveCreator_ShouldCallSaveWithCorrectParametersOnce()
    {
        // Given
        
        Usuario user = UserMother.CreateRandom();
        VehicleResponse vehicleResponse = VehicleResponseMother.CreateRandom();
        user.Vehicle = vehicleResponse;
        
        HttpResponseMessage message = new HttpResponseMessage();
        message.StatusCode = HttpStatusCode.OK;
        message.Content = new StringContent(JsonConvert.SerializeObject(vehicleResponse));
        
        this.ShouldSaveUsers(user);
        this.ShouldFindVehicleByHttp(message);
        // When
        this._userActiveCreator.Execute(user.Id, user.Name, user.Email, vehicleResponse.Id);
        // Then 
        this.ShouldHaveCalledSaveWithCorrectParametersOnce(user);
    }
    [Fact]
    public void ShouldCallGetAsyncWithCorrectParametersOnce()
    {
        // GIVEN
        Usuario user = UserMother.CreateRandom();
        VehicleResponse vehicleResponse = VehicleResponseMother.CreateRandom();
        user.Vehicle = vehicleResponse;
        
        HttpResponseMessage message = new HttpResponseMessage();
        message.StatusCode = HttpStatusCode.OK;
        message.Content = new StringContent(JsonConvert.SerializeObject(vehicleResponse));
        
        this.ShouldSaveUsers(user);
        this.ShouldFindVehicleByHttp(message);
        // WHEN
        this._userActiveCreator.Execute(user.Id, user.Name, user.Email, vehicleResponse.Id);
        // THEN
        this.ShouldHaveCalledGetAsyncWithCorrectParametersOnce();
    }
    
    /*[Fact]
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
    }*/

    /*[Fact]
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
    }*/

    /*[Fact]
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
    }*/
    
}