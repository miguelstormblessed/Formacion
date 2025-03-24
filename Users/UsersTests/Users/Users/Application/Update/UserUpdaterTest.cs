using System.Net;
using Newtonsoft.Json;
using Users.Shared.Vehicles.Domain.Responses;
using Users.Users.Application.Find;
using Users.Users.Application.Update;
using Users.Users.Domain;
using UsersTests.Users.Shared.Vehicles.Domain.Responses;
using UsersTests.Users.Users.Domain;

namespace UsersTests.Users.Users.Application.Update;

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
        VehicleResponse vehicleResponse = VehicleResponseMother.CreateRandom();
        user.Vehicle = vehicleResponse;
        
        HttpResponseMessage message = new HttpResponseMessage();
        message.StatusCode = HttpStatusCode.OK;
        message.Content = new StringContent(JsonConvert.SerializeObject(vehicleResponse));
        
        this.ShouldFindUser(user.Id, user);
        this.ShouldFindVehicleByHttp(message);
        // WHEN
        this._userUpdater.Execute(user.Id, user.Name, user.Email, user.State, vehicleResponse);
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