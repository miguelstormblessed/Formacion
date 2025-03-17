using Cojali.Shared.Domain.Bus.Event;
using UsersManagement.Shared.Users.Domain.Exceptions;
using UsersManagement.Users.Application.Find;
using UsersManagement.Users.Domain;
using UsersManagement.Users.Domain.ValueObject;

namespace UsersManagement.Users.Application.Delete;

public class UserDeleter
{
    private readonly IUserRepository _userRepository;
    private readonly UserFinder _userFinder;
    private readonly IEventBus _eventBus;
    public UserDeleter(IUserRepository userRepository, UserFinder userFinder, IEventBus eventBus)
    {
        this._userRepository = userRepository;
        this._userFinder = userFinder;
        this._eventBus = eventBus;
    }

    public void Execute(UserId userId)
    {
        Usuario user = this._userFinder.Execute(userId);
        if (!user.State.Active)
        {
            throw new UserAlreadyDeletedException();
        }
        
        user.Delete(user.Id, user.Name, user.Email);
        
        this._userRepository.Delete(user.Id);
        this._eventBus.PublishAsync(user.PullDomainEvents());
        
    }
}