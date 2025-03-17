using Cojali.Shared.Domain.Bus.Query;
using UsersManagement.Bookings.Domain;
using UsersManagement.Bookings.Domain.Specification;
using UsersManagement.Shared.Users.Domain.Querys;
using UsersManagement.Shared.Users.Domain.Responses;

namespace UsersManagement.Bookings.Application.Search;

public class BookingSearcherByUser
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IQueryBus _queryBus;

    public BookingSearcherByUser(IBookingRepository bookingRepository, IQueryBus queryBus)
    {
        _bookingRepository = bookingRepository;
        _queryBus = queryBus;
    }

    public async Task<IEnumerable<Booking>> ExecuteAsync(string userId)
    {
        UserFinderQuery userFinderQuery = UserFinderQuery.Create(userId);
        await _queryBus.AskAsync(userFinderQuery);
        return await _bookingRepository.Search(new BookingByUserIdSpecification(userId));
    }
}