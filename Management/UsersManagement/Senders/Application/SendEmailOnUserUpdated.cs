using Cojali.Shared.Domain.Bus.Event;
using UsersManagement.Senders.Domain;
using UsersManagement.Senders.Domain.ValueObject;
using UsersManagement.Shared.Users.Domain.DomainEvents;

namespace UsersManagement.Senders.Application;

public class SendEmailOnUserUpdated : IDomainEventSubscriber<UserUpdated>
{
    private readonly Sender _sender;
    public SendEmailOnUserUpdated(Sender sender) => _sender = sender;
    public Task OnAsync(UserUpdated domainEvent)
    {
        return Task.CompletedTask;
    }

    public Task OnAsync(DomainEvent genericEvent)
    {
        UserUpdated userUpdatedDomainEvent = (UserUpdated)genericEvent;
        Send send = Send.Create(
            SendTo.Create(userUpdatedDomainEvent.Email),
            SendSubject.Create("User Updated"),
            SendMessage.Create("User has been updated"));
        this._sender.Execute(send);
        
        return Task.CompletedTask;
    }
}