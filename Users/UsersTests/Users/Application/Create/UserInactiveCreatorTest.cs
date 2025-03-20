using System.Net;
using Newtonsoft.Json;
using Users.Shared.Vehicles.Domain.Responses;
using Users.Users.Application.Create;
using Users.Users.Domain;
using UsersTests.Shared.Vehicles.Domain.Responses;
using UsersTests.Users.Domain;

namespace UsersTests.Users.Application.Create;

public class UserInactiveCreatorTest : UserModuleApplicationUnitTestCase
{
    private readonly UserInactiveCreator _userInactiveCreator;
    public UserInactiveCreatorTest()
    {
        this._userInactiveCreator = new UserInactiveCreator(this.UserRepository.Object, this.EventBus.Object, this.QueryBus.Object, this.CommandBus.Object, this.HttpClientService.Object);
    }
    
    
    [Fact]
    public void UserInactiveCreator_ShouldCallGetAsyncWithCorrectParametersOnce()
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
        this._userInactiveCreator.Execute(user.Id, user.Name, user.Email, vehicleResponse.Id);
        // THEN
        this.ShouldHaveCalledGetAsyncWithCorrectParametersOnce();
    }

    [Fact]
    public void ShouldCallSaveWithCorrectParametersOnce()
    {
        // Given
        
        Usuario user = UserMother.CreateRandom();
        user.State.Active = false;
        VehicleResponse vehicleResponse = VehicleResponseMother.CreateRandom();
        user.Vehicle = vehicleResponse;
        
        HttpResponseMessage message = new HttpResponseMessage();
        message.StatusCode = HttpStatusCode.OK;
        message.Content = new StringContent(JsonConvert.SerializeObject(vehicleResponse));
        
        this.ShouldSaveUsers(user);
        this.ShouldFindVehicleByHttp(message);
        // When
        this._userInactiveCreator.Execute(user.Id, user.Name, user.Email, vehicleResponse.Id);
        // Then 
        this.ShouldHaveCalledSaveWithCorrectParametersOnce(user);
    }
    /*[Fact]
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
    }*/
}