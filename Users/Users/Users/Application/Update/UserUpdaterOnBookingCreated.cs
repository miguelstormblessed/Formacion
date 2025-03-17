using Cojali.Shared.Domain.Bus.Event;
using UsersManagement.Shared.Bookings.Domain.DomainEvents;
using UsersManagement.Users.Application.Find;
using UsersManagement.Users.Domain;
using UsersManagement.Users.Domain.ValueObject;

namespace UsersManagement.Users.Application.Update;

public class UserUpdaterOnBookingCreated : IDomainEventSubscriber<BookingCreated>
{
    public UserUpdaterOnBookingCreated(UserUpdater updater, UserFinder finder)
    {
        _updater = updater;
        _userFinder = finder;
    }

    public readonly UserUpdater _updater;
    public UserFinder _userFinder;
    
    public Task OnAsync(BookingCreated domainEvent)
    {
        return Task.CompletedTask;
    }

    public Task OnAsync(DomainEvent genericEvent)
    {
        BookingCreated bookingCreatedDomainEvent = (BookingCreated)genericEvent;
        UserId userId = UserId.Create(bookingCreatedDomainEvent.UserId);
        Usuario user = _userFinder.Execute(userId);
        _updater.Execute(user.Id, user.Name, user.Email, user.State, bookingCreatedDomainEvent.VehicleId);
        
        return Task.CompletedTask;
    }
}