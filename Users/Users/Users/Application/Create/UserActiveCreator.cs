using System.Net;
using Cojali.Shared.Domain.Bus.Command;
using Cojali.Shared.Domain.Bus.Event;
using Cojali.Shared.Domain.Bus.Query;
using Users.Shared.Vehicles.Domain.Exceptions;
using Users.Shared.Vehicles.Domain.Responses;
using Users.Users.Domain;
using Users.Users.Domain.ValueObject;
using Users.Users.Infrastructure.Mappers;

namespace Users.Users.Application.Create;

public class UserActiveCreator
{
    private readonly IUserRepository _userRepository;
    private readonly IEventBus _eventBus;
    private readonly IQueryBus _queryBus;
    private readonly ICommandBus _commandBus;
    
    public UserActiveCreator(IUserRepository userRepository, IEventBus eventBus, IQueryBus queryBus, ICommandBus commandBus)
    {
        _userRepository = userRepository;
        this._eventBus = eventBus;
        this._queryBus = queryBus;
        this._commandBus = commandBus;
    }

    public async Task Execute(UserId userId, UserName userName, UserEmail userEmail, VehicleResponse vehicleResponse)
    {
        
        Usuario user = Usuario.CreateUserActivated(userId, userName, userEmail, vehicleResponse);
        this._userRepository.Save(user);
        
        
    }
}