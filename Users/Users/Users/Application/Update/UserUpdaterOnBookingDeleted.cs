using Cojali.Shared.Domain.Bus.Event;
using Users.Shared.Bookings.Domain.DomainEvents;
using UsersManagement.Users.Application.Find;
using UsersManagement.Users.Domain;
using UsersManagement.Users.Domain.ValueObject;

namespace UsersManagement.Users.Application.Update;

public class UserUpdaterOnBookingDeleted : IDomainEventSubscriber<BookingDeleted>
{
    private readonly UserUpdater _userUpdater;
    private readonly UserFinder _userFinder;

    public UserUpdaterOnBookingDeleted(UserUpdater userUpdater, UserFinder userFinder)
    {
        _userUpdater = userUpdater;
        _userFinder = userFinder;
    }

    public Task OnAsync(BookingDeleted domainEvent)
    {
        return Task.CompletedTask;
    }

    public async Task OnAsync(DomainEvent genericDomainEvent)
    {
        BookingDeleted bookingDeleted = (BookingDeleted)genericDomainEvent;
        UserId userId = UserId.Create(bookingDeleted.UserId);
        Usuario user = _userFinder.Execute(userId);
        await _userUpdater.Execute(user.Id, user.Name, user.Email, user.State, null);
    }
}                