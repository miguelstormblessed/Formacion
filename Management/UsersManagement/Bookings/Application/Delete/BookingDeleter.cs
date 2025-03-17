using Cojali.Shared.Domain.Bus.Command;
using Cojali.Shared.Domain.Bus.Event;
using UsersManagement.Bookings.Application.Find;
using UsersManagement.Bookings.Domain;
using UsersManagement.Bookings.Domain.ValueObject;
using UsersManagement.Shared.Users.Domain.Commands;

namespace UsersManagement.Bookings.Application.Delete;

public class BookingDeleter
{
    private readonly IBookingRepository _bookingRepository;
    private readonly BookingFinder _bookingFinder;
    private readonly ICommandBus _commandBus;
    public BookingDeleter(IBookingRepository bookingRepository, BookingFinder bookingFinder, ICommandBus commandBus)
    {
        _bookingRepository = bookingRepository;
        _bookingFinder = bookingFinder;
        _commandBus = commandBus;
    }

    public void Execute(BookingId bookingId)
    {
        Booking? booking = _bookingFinder.Execute(bookingId);
        UserUpdaterCommand userUpdaterCommand = UserUpdaterCommand.Create(
            booking.UserResponse.Id, 
            booking.UserResponse.Name,
            booking.UserResponse.Email,
            booking.UserResponse.State,
            null);
        //booking?.Delete();
        this._bookingRepository.Delete(bookingId);
        //this._eventBus.PublishAsync(booking?.PullDomainEvents());
        this._commandBus.DispatchAsync(userUpdaterCommand);
        
    }
}