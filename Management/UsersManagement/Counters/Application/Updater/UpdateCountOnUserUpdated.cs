using Cojali.Shared.Domain.Bus.Event;
using UsersManagement.Shared.Users.Domain.DomainEvents;

namespace UsersManagement.Counters.Application.Updater;

public class UpdateCountOnUserUpdated : IDomainEventSubscriber<UserUpdated>
{
    private readonly UsersCounterUpdater _usersCounterUpdater;

    public UpdateCountOnUserUpdated(UsersCounterUpdater usersCounterUpdater)
    {
        this._usersCounterUpdater = usersCounterUpdater;
    }

    public Task OnAsync(UserUpdated domainEvent)
    {
        return Task.CompletedTask;
    }

    public Task OnAsync(DomainEvent genericEvent)
    {
        UserUpdated domainEvent = (UserUpdated)genericEvent;

        if (domainEvent.OldState != domainEvent.State)
        {
            int activeChange = domainEvent.State ? 1 : -1;
            int inactiveChange = -activeChange;
    
            this._usersCounterUpdater.Execute(inactiveChange, activeChange);
        }
        else
        {
            this._usersCounterUpdater.Execute(0, 0);
        }

        return Task.CompletedTask;
    }
}