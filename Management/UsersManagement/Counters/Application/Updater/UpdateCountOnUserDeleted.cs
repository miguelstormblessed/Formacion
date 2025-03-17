using Cojali.Shared.Domain.Bus.Event;
using UsersManagement.Shared.Users.Domain.DomainEvents;

namespace UsersManagement.Counters.Application.Updater;

public class UpdateCountOnUserDeleted : IDomainEventSubscriber<UserDeleted>
{
    private readonly UsersCounterUpdater _updater;

    public UpdateCountOnUserDeleted(UsersCounterUpdater updater)
    {
        _updater = updater;
    }

    public Task OnAsync(UserDeleted domainEvent)
    {
        return Task.CompletedTask;
    }

    public Task OnAsync(DomainEvent @event)
    {
        this._updater.Execute(-1, 1);
        return Task.CompletedTask;
    }
}