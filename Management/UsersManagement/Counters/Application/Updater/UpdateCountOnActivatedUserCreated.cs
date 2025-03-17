using Cojali.Shared.Domain.Bus.Event;
using UsersManagement.Shared.Users.Domain.DomainEvents;

namespace UsersManagement.Counters.Application.Updater;

public class UpdateCountOnActivatedUserCreated : IDomainEventSubscriber<ActivatedUserCreated>
{
    private readonly UsersCounterUpdater _updater;
    public UpdateCountOnActivatedUserCreated(UsersCounterUpdater updater) => _updater = updater;
    public Task OnAsync(ActivatedUserCreated domainEvent)
    {
        return Task.CompletedTask;
    }

    public Task OnAsync(DomainEvent genericEvent)
    {
        this._updater.Execute(1,0);
        return Task.CompletedTask;
    }
}