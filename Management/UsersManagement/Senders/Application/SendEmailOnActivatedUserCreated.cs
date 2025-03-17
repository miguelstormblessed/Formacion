using Cojali.Shared.Domain.Bus.Event;
using UsersManagement.Senders.Domain;
using UsersManagement.Senders.Domain.ValueObject;
using UsersManagement.Shared.Users.Domain.DomainEvents;

namespace UsersManagement.Senders.Application;

public class SendEmailOnActivatedUserCreated : IDomainEventSubscriber<ActivatedUserCreated>
{
    public readonly Sender _sender;
    
    public SendEmailOnActivatedUserCreated(Sender sender) => _sender = sender;
    
    
    public Task OnAsync(ActivatedUserCreated activatedUserCreated)
    {
        return Task.CompletedTask;
    }

    public Task OnAsync(DomainEvent genericEvent)
    {
        ActivatedUserCreated activatedUserCreatedDomainEvent = (ActivatedUserCreated)genericEvent;
        
        Send send = Send.Create(
            SendTo.Create(activatedUserCreatedDomainEvent.Email),
            SendSubject.Create("User Created"),
            SendMessage.Create("Se ha creado un usuario"));
        
        this._sender.Execute(send);
        
        return Task.CompletedTask;
    }
}