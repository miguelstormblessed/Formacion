using Cojali.Shared.Domain.Bus.Command;
using Cojali.Shared.Domain.Bus.Event;
using Cojali.Shared.Domain.Bus.Query;
using Users.Shared.Vehicles.Domain.Commands;
using Users.Shared.Vehicles.Domain.Exceptions;
using Users.Shared.Vehicles.Domain.Querys;
using Users.Shared.Vehicles.Domain.Responses;
using UsersManagement.Users.Domain;
using UsersManagement.Users.Domain.ValueObject;

namespace UsersManagement.Users.Application.Create;

public class UserInactiveCreator
{
    private readonly IUserRepository _userRepository;
    private readonly IEventBus _eventBus;
    private readonly IQueryBus _queryBus;
    private readonly ICommandBus _commandBus;
    public UserInactiveCreator(IUserRepository userRepository, IEventBus eventBus, IQueryBus queryBus, ICommandBus commandBus)
    {
        this._userRepository = userRepository;
        this._eventBus = eventBus;
        this._queryBus = queryBus;
        this._commandBus = commandBus;
    }

    public async Task Execute(UserId userId, UserName userName, UserEmail userEmail, string vehicleId)
    {
        VehicleFinderQuery query = VehicleFinderQuery.Create(vehicleId);
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
            
        }
        Usuario user = Usuario.CreateUserDeactivated(userId, userName,userEmail, vehicle);
        
        this._userRepository.Save(user);
        this._eventBus.PublishAsync(user.PullDomainEvents());
    }
}