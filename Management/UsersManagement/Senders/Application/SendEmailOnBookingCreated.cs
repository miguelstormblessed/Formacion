using Cojali.Shared.Domain.Bus.Event;
using UsersManagement.Senders.Domain;
using UsersManagement.Senders.Domain.ValueObject;
using UsersManagement.Shared.Bookings.Domain.DomainEvents;

namespace UsersManagement.Senders.Application;

public class SendEmailOnBookingCreated : IDomainEventSubscriber<BookingCreated>
{
    private readonly Sender _sender;

    public SendEmailOnBookingCreated(Sender sender)
    {
        _sender = sender;
    }

    public Task OnAsync(BookingCreated domainEvent)
    {
        return Task.CompletedTask;
    }

    public Task OnAsync(DomainEvent genericEvent)
    {
        BookingCreated bookingCreatedDomainEvent = (BookingCreated) genericEvent;
        
        Send send = Send.Create(
            SendTo.Create(bookingCreatedDomainEvent.Email),
            SendSubject.Create("Booking Created"),
            SendMessage.Create("Booking has been created and confirmed"));
        this._sender.Execute(send);
        
        return Task.CompletedTask;
    }
}