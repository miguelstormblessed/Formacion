using Cojali.Shared.Domain.Bus.Command;
using Cojali.Shared.Domain.Bus.Event;
using Cojali.Shared.Domain.Bus.Query;
using Users.Shared.HttpClient;
using Users.Shared.Vehicles.Domain.Commands;
using Users.Shared.Vehicles.Domain.Exceptions;
using Users.Shared.Vehicles.Domain.Querys;
using Users.Shared.Vehicles.Domain.Responses;
using UsersManagement.Users.Domain;
using UsersManagement.Users.Domain.ValueObject;
using UsersManagement.Users.Infrastructure.Mappers;

namespace UsersManagement.Users.Application.Create;

public class UserActiveCreator
{
    private readonly IUserRepository _userRepository;
    private readonly IEventBus _eventBus;
    private readonly IQueryBus _queryBus;
    private readonly ICommandBus _commandBus;
    private readonly IHttpClientService _httpClientService;
    
    public UserActiveCreator(IUserRepository userRepository, IEventBus eventBus, IQueryBus queryBus, ICommandBus commandBus, IHttpClientService httpClientService)
    {
        _userRepository = userRepository;
        this._eventBus = eventBus;
        this._queryBus = queryBus;
        this._commandBus = commandBus;
        this._httpClientService = httpClientService;
    }

    public async Task Execute(UserId userId, UserName userName, UserEmail userEmail, string vehicleId)
    {
        /*VehicleFinderQuery query = VehicleFinderQuery.Create(vehicleId);
        VehicleCreatorCommand creatorCommand = VehicleCreatorCommand.Create(vehicleId);
        VehicleResponse? vehicle = null;
        try
        {
            vehicle = await this._queryBus.AskAsync(query);
            
        }
        catch (VehicleNotFoundException e)
        {
            await this._commandBus.DispatchAsync(creatorCommand);
            vehicle = await this._queryBus.AskAsync(query);
            
        }*/

        HttpResponseMessage response =
            await this._httpClientService.GetAsync($"https://localhost:7239/VehicleFinder?id={vehicleId}");
        
        VehicleResponse? vehicle = VehicleResponse.FromJson(response.Content.ReadAsStringAsync().Result);
        
        Usuario user = Usuario.CreateUserActivated(userId, userName, userEmail, vehicle);
        this._userRepository.Save(user);
        //await this._eventBus.PublishAsync(user.PullDomainEvents());
        
        
    }
}