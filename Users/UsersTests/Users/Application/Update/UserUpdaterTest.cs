using System.Net;
using Moq;
using Newtonsoft.Json;
using Users.Shared.HttpClient;
using Users.Shared.Users.Domain.DomainEvents;
using Users.Shared.Vehicles.Domain.Responses;
using UsersManagement.Users.Application.Find;
using UsersManagement.Users.Application.Update;
using UsersManagement.Users.Domain;
using UsersTests.Shared.Vehicles.Domain.Responses;
using UsersTests.Users.Domain;

namespace UsersTests.Users.Application.Update;

public class UserUpdaterTest : UserModuleApplicationUnitTestCase
{
    private readonly UserUpdater _userUpdater;
    private readonly UserFinder _userFinder;
    public UserUpdaterTest()
    {   
        this._userFinder = new UserFinder(this.UserRepository.Object);
        this._userUpdater = new UserUpdater(this.UserRepository.Object, this._userFinder,this.EventBus.Object, this.QueryBus.Object, this.HttpClientService.Object);
    }
    [Fact]
    public void ShouldCallUserUpdateOnce()
    {
        // GIVEN
        Usuario user = UserMother.CreateRandom();
        VehicleResponse vehicleResponse = VehicleResponseMother.CreateRandom();
        user.Vehicle = vehicleResponse;
        
        HttpResponseMessage message = new HttpResponseMessage();
        message.StatusCode = HttpStatusCode.OK;
        message.Content = new StringContent(JsonConvert.SerializeObject(vehicleResponse));
        
        this.ShouldFindUser(user.Id, user);
        this.ShouldFindVehicleByHttp(message);
        // WHEN
        this._userUpdater.Execute(user.Id, user.Name, user.Email, user.State, Guid.NewGuid().ToString());
        // THEN
        this.ShouldHaveCalledUpdateWithCorrectParametersOnce(user);

    }

    /*[Fact]
    public void ShouldCallPublishOnceWithCorrectParameters()
    {
        // GIVEN
        Usuario user = UserMother.CreateRandom();
        this.ShouldFindUser(user.Id, user);
        
        // WHEN
        this._userUpdater.Execute(user.Id, user.Name, user.Email, user.State, Guid.NewGuid().ToString());
        // THEN
        this.ShouldHaveCalledPublishWithcorrectParametersOnce<UserUpdated>();
    }*/
   
}