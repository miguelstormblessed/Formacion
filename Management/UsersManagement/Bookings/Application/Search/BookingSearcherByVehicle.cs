using Cojali.Shared.Domain.Bus.Query;
using UsersManagement.Bookings.Domain;
using UsersManagement.Bookings.Domain.Specification;
using UsersManagement.Shared.Vehicles.Domain.Querys;
using UsersManagement.Vehicles.Application.Find;

namespace UsersManagement.Bookings.Application.Search;

public class BookingSearcherByVehicle
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IQueryBus _queryBus;
    public BookingSearcherByVehicle(IBookingRepository bookingRepository, IQueryBus queryBus)
    {
        _bookingRepository = bookingRepository;
        _queryBus = queryBus;
    }

    public async Task<IEnumerable<Booking>> ExecuteAsync(string vehicleId)
    {
        VehicleFinderQuery vehicleFinderQuery = VehicleFinderQuery.Create(vehicleId);
        await this._queryBus.AskAsync(vehicleFinderQuery);
        return await _bookingRepository.Search(new BookingByVehicleIdSpecification(vehicleId));
    }
}