using Cojali.Shared.Domain.Bus.Event;
using UsersManagement.Senders.Domain;
using UsersManagement.Senders.Domain.ValueObject;
using UsersManagement.Shared.Users.Domain.DomainEvents;

namespace UsersManagement.Senders.Application;

public class SendEmailOnUserDeleted : IDomainEventSubscriber<UserDeleted>
{
    private readonly Sender _sender;
    public SendEmailOnUserDeleted(Sender sender) => _sender = sender;
    public Task OnAsync(UserDeleted domainEvent)
    {
        return Task.CompletedTask;
    }

    public Task OnAsync(DomainEvent genericEvent)
    {
        UserDeleted domainEvent = (UserDeleted)genericEvent;
        Send send = Send.Create(
            SendTo.Create(domainEvent.Email),
            SendSubject.Create("User Deleted"),
            SendMessage.Create("User has been deleted"));
        this._sender.Execute(send);
        return Task.CompletedTask;
    }
}