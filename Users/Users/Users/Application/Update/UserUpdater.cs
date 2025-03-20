using System.Net;
using Cojali.Shared.Domain.Bus.Event;
using Cojali.Shared.Domain.Bus.Query;
using Users.Shared.Vehicles.Domain.Exceptions;
using Users.Shared.Vehicles.Domain.Responses;
using Users.Users.Application.Find;
using Users.Users.Domain;
using Users.Users.Domain.ValueObject;

namespace Users.Users.Application.Update;

public class UserUpdater
{
    private readonly IUserRepository _userRepository;
    private readonly UserFinder _userFinder;
    private readonly IEventBus _eventBus;
    private readonly IQueryBus _queryBus;
    private readonly IHttpClientService _httpClientService;

    public UserUpdater(IUserRepository userRepository, UserFinder userFinder, IEventBus eventBus, IQueryBus queryBus, IHttpClientService httpClientService)
    {
        this._userRepository = userRepository;
        this._userFinder = userFinder;
        this._eventBus = eventBus;
        this._queryBus = queryBus;
        this._httpClientService = httpClientService;
    }

    public async Task Execute(UserId userId, UserName newUserName, UserEmail newUserEmail, UserState newUserState, string? vehicleId)
    {
        Usuario user = this._userFinder.Execute(userId);
        /*VehicleResponse? vehicle = null;
        if (vehicleId != null)
        {
            vehicle = await this._queryBus.AskAsync(VehicleFinderQuery.Create(vehicleId));
        }*/
        
        HttpResponseMessage response =
            await this._httpClientService.GetAsync($"https://localhost:7239/VehicleFinder?id={vehicleId}");
        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            throw new VehicleNotFoundException();
        }
        
        VehicleResponse? vehicle = VehicleResponse.FromJson(response.Content.ReadAsStringAsync().Result);

        user.Update(userId, newUserName, newUserEmail, newUserState, vehicle);
        
        _userRepository.Update(user);
        //await this._eventBus.PublishAsync(user.PullDomainEvents());
        

    }
}