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
    private readonly IHttpClientService _client;

    public BookingSearcherByUser(IBookingRepository bookingRepository, IQueryBus queryBus, IHttpClientService client)
    {
        _bookingRepository = bookingRepository;
        _queryBus = queryBus;
        _client = client;
    }

    public async Task<IEnumerable<Booking>> ExecuteAsync(string userId)
    {
        var result = await _client.GetAsync($"https://localhost:7172/UserFinder?id={userId}");
        CheckStatusCode(result);
        var content = result.Content.ReadAsStringAsync().Result;
        
        UserResponse userResponse = UserResponse.FromJson(content);
        return await _bookingRepository.Search(new BookingByUserIdSpecification(userId));
    }

    private void CheckStatusCode(HttpResponseMessage response)
    {
        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            throw new UserNotFoundException();
        }
    }
}