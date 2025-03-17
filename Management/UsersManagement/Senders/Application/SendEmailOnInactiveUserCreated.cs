using Cojali.Shared.Domain.Bus.Event;
using Cojali.Shared.Domain.Repository;
using UsersManagement.Senders.Domain;
using UsersManagement.Senders.Domain.ValueObject;
using UsersManagement.Shared.Users.Domain.DomainEvents;

namespace UsersManagement.Senders.Application;

public class SendEmailOnInactiveUserCreated : IDomainEventSubscriber<InactiveUserCreated>
{
    public readonly Sender _sender;
    
    public SendEmailOnInactiveUserCreated(Sender sender) => _sender = sender;
    
    public Task OnAsync(InactiveUserCreated domainEvent)
    {
        return Task.CompletedTask;
    }

    public Task OnAsync(DomainEvent genericEvent)
    {
        InactiveUserCreated inactiveUserCreatedDomainEvent = (InactiveUserCreated)genericEvent;
        Send send = Send.Create(
            SendTo.Create(inactiveUserCreatedDomainEvent.Email),
            SendSubject.Create("User Inactive Created"),
            SendMessage.Create("An inactive user has been created"));
        this._sender.Execute(send);
        return Task.CompletedTask;
    }
}