using Cojali.Shared.Domain.Specification;
using UsersManagement.Bookings.Domain;
using UsersManagement.Bookings.Domain.Specification;
using UsersManagement.Shared.Bookings.Domain.Requests;

namespace UsersManagement.Bookings.Application.Search;

public class BookingSearcher
{
    private readonly IBookingRepository _bookingRepository;

    public BookingSearcher(IBookingRepository bookingRepository)
    {
        _bookingRepository = bookingRepository;
    }

    public async Task<IEnumerable<Booking>>ExecuteAsync()
    {
        return await this._bookingRepository.SearchAll();
    }
    
}