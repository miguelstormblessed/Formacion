using Cojali.Shared.Domain.Bus.Event;
using UsersManagement.Shared.Users.Domain.DomainEvents;

namespace UsersManagement.Counters.Application.Updater;

public class UpdateCountOnInactiveUserCreated : IDomainEventSubscriber<InactiveUserCreated>
{
    private readonly UsersCounterUpdater _usersCounterUpdater;

    public UpdateCountOnInactiveUserCreated(UsersCounterUpdater usersCounterUpdater)
    {
        _usersCounterUpdater = usersCounterUpdater;
    }

    public Task OnAsync(InactiveUserCreated domainEvent)
    {
        return Task.CompletedTask;
    }

    public Task OnAsync(DomainEvent @event)
    {
        this._usersCounterUpdater.Execute(0, 1);
        return Task.CompletedTask;
    }
}