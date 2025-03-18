using System.Net;
using System.Net.Http.Json;
using Cojali.Shared.Domain.Bus.Command;
using Cojali.Shared.Domain.Bus.Event;
using Cojali.Shared.Domain.Bus.Query;
using MySqlX.XDevAPI;
using UsersManagement.Bookings.Domain;
using UsersManagement.Bookings.Domain.ValueObject;
using UsersManagement.Shared.HttpClient;
using UsersManagement.Shared.Users.Domain.Commands;
using UsersManagement.Shared.Users.Domain.Exceptions;
using UsersManagement.Shared.Users.Domain.Querys;
using UsersManagement.Shared.Users.Domain.Requests;
using UsersManagement.Shared.Users.Domain.Responses;
using UsersManagement.Shared.Vehicles.Domain.Querys;
using UsersManagement.Shared.Vehicles.Domain.Responses;

namespace UsersManagement.Bookings.Application.Create;

public class BookingCreator
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IQueryBus _queryBus;
    private readonly ICommandBus _commandBus;
    private readonly IHttpClientService _client;

    public BookingCreator(IBookingRepository bookingRepository, IQueryBus queryBus, ICommandBus commandBus, IHttpClientService client)
    {
        _client = client;
        _bookingRepository = bookingRepository;
        _queryBus = queryBus;
        _commandBus = commandBus;
    }

    public async Task Execute(BookingId bookingId, BookingDate bookingDate, string vehicleId, string userId)
    {
        // Querys
        HttpResponseMessage result = await _client.GetAsync($"https://localhost:7172/UserFinder?id={userId}");
        CheckStatusCode(result);
        
        string  content = result.Content.ReadAsStringAsync().Result;
        
        UserResponse userResponse = UserResponse.FromJson(content);
        
        VehicleFinderQuery vehicleFinderQuery = VehicleFinderQuery.Create(vehicleId);
        VehicleResponse vehicleResponse = await this._queryBus.AskAsync(vehicleFinderQuery);
        UserUpdaterCommand userUpdaterCommand = UserUpdaterCommand.Create(
            userResponse.Id,
            userResponse.Name,
            userResponse.Email,
            userResponse.State,
            vehicleResponse.Id
            );

        Booking booking = Booking.Create(bookingId, bookingDate, vehicleResponse, userResponse);
        this._bookingRepository.Save(booking);
        //await this._commandBus.DispatchAsync(userUpdaterCommand);
    }
    private void CheckStatusCode(HttpResponseMessage response)
    {
        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            throw new UserNotFoundException();
        }
    }
}