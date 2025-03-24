using System.Net;
using Cojali.Shared.Domain.Bus.Query;
using UsersManagement.Bookings.Domain;
using UsersManagement.Bookings.Domain.Specification;
using UsersManagement.Shared.HttpClient;
using UsersManagement.Shared.Users.Domain.Exceptions;
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
        /*var result = await _client.GetAsync($"https://localhost:7172/UserFinder?id={userId}");
        CheckStatusCode(result);
        var content = result.Content.ReadAsStringAsync().Result;*/
        
        //UserFinderQuery userFinderQuery = UserFinderQuery.Create(userId);
        //UserResponse userResponse = await this._queryBus.AskAsync(userFinderQuery);
        /*if (userResponse == null)
        {
            throw new UserNotFoundException();
        }*/
        return await _bookingRepository.Search(new BookingByUserIdSpecification(userId));
    }

    
}