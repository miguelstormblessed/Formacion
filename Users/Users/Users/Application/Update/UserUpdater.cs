using Cojali.Shared.Domain.Bus.Event;
using Cojali.Shared.Domain.Bus.Query;
using UsersManagement.Shared.Vehicles.Domain.Querys;
using UsersManagement.Shared.Vehicles.Domain.Responses;
using UsersManagement.Users.Application.Find;
using UsersManagement.Users.Domain;
using UsersManagement.Users.Domain.ValueObject;

namespace UsersManagement.Users.Application.Update;

public class UserUpdater
{
    private readonly IUserRepository _userRepository;
    private readonly UserFinder _userFinder;
    private readonly IEventBus _eventBus;
    private readonly IQueryBus _queryBus;

    public UserUpdater(IUserRepository userRepository, UserFinder userFinder, IEventBus eventBus, IQueryBus queryBus)
    {
        this._userRepository = userRepository;
        this._userFinder = userFinder;
        this._eventBus = eventBus;
        this._queryBus = queryBus;
    }

    public async Task Execute(UserId userId, UserName newUserName, UserEmail newUserEmail, UserState newUserState, string? vehicleId)
    {
        Usuario user = this._userFinder.Execute(userId);
        VehicleResponse? vehicle = null;
        if (vehicleId != null)
        {
            vehicle = await this._queryBus.AskAsync(VehicleFinderQuery.Create(vehicleId));
        }
        user.Update(userId, newUserName, newUserEmail, newUserState, vehicle);
        
        _userRepository.Update(user);
        await this._eventBus.PublishAsync(user.PullDomainEvents());
        

    }
}