using System.Net;
using Cojali.Shared.Domain.Bus.Command;
using Cojali.Shared.Domain.Bus.Query;
using Users.Users.Application.Find;
using Users.Users.Domain;
using Users.Users.Domain.ValueObject;
using Users.Users.Infrastructure;
using UsersManagement.Bookings.Domain;
using UsersManagement.Bookings.Domain.ValueObject;
using UsersManagement.Shared.Users.Domain.Commands;
using UsersManagement.Shared.Users.Domain.Querys;
using UsersManagement.Shared.Users.Domain.Responses;
using UsersManagement.Shared.Vehicles.Domain.Querys;
using UsersManagement.Shared.Vehicles.Domain.Responses;

namespace UsersManagement.Bookings.Application.Create;

public class BookingCreator
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IQueryBus _queryBus;
    private readonly ICommandBus _commandBus;

    public BookingCreator(IBookingRepository bookingRepository, IQueryBus queryBus, ICommandBus commandBus)
    {
        _bookingRepository = bookingRepository;
        _queryBus = queryBus;
        _commandBus = commandBus;
    }

    public async Task Execute(BookingId bookingId, BookingDate bookingDate, string vehicleId, UserResponse userResponse)
    {
        // Querys
        //UserFinderQuery userFinderQuery = UserFinderQuery.Create(userId);
        VehicleFinderQuery vehicleFinderQuery = VehicleFinderQuery.Create(vehicleId);
        //Usuario user = _userFinder.Execute(UserId.Create(userId)); 
        VehicleResponse vehicleResponse = await this._queryBus.AskAsync(vehicleFinderQuery);

        //UserResponse userResponse = await _queryBus.AskAsync(userFinderQuery);

        /*UserUpdaterCommand userUpdaterCommand = UserUpdaterCommand.Create(
            userResponse.Id,
            userResponse.Name,
            userResponse.Email,
            userResponse.State,
            vehicleResponse.Id
            );*/

        Booking booking = Booking.Create(bookingId, bookingDate, vehicleResponse, userResponse);
        this._bookingRepository.Save(booking);
        //await this._commandBus.DispatchAsync(userUpdaterCommand);
    }
}