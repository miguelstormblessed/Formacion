using Moq;
using UsersManagement.Bookings.Application.Create;
using UsersManagement.Bookings.Domain;
using UsersManagement.Bookings.Domain.ValueObject;
using UsersManagement.Shared.Bookings.Domain.DomainEvents;
using UsersManagement.Shared.Users.Domain.Commands;
using UsersManagement.Shared.Users.Domain.Querys;
using UsersManagement.Shared.Users.Domain.Responses;
using UsersManagement.Shared.Vehicles.Domain.Querys;
using UsersManagement.Shared.Vehicles.Domain.Responses;
using UsersTests.UsersManagement.Bookings.Domain;
using UsersTests.UsersManagement.Bookings.Domain.ValueObject;
using UsersTests.UsersManagement.Shared.Users.Responses;
using UsersTests.UsersManagement.Shared.Vehicles.Domain.Responses;

namespace UsersTests.UsersManagement.Bookings.Application.Create;

public class BookingCreatorTest : BookingsModuleApplicationTestCase
{
    private readonly BookingCreator _bookingCreator;

    public BookingCreatorTest()
    {
        _bookingCreator = new BookingCreator(this.BookingRepository.Object, this.QueryBus.Object, this.CommandBus.Object);
    }

    [Fact]
    public async Task ShouldCallAskAsyncWithCorrectVehicleFinderQueryParameters()
    {
        // GIVEN
        VehicleResponse vehicleResponse = VehicleResponseMother.CreateRandom();
        UserResponse userResponse = UserResponseMother.CreateRandom();
        VehicleFinderQuery vehicleFinderQuery = VehicleFinderQuery.Create(vehicleResponse.Id);
        UserFinderQuery userFinderQuery = UserFinderQuery.Create(userResponse.Id);
        Booking booking = BookingMother.CreateRandom();
        this.ShouldReturnVehicleResponseThenUserResponse(vehicleFinderQuery, vehicleResponse, userFinderQuery, userResponse);
        // WHEN
        await _bookingCreator.Execute(booking.Id, booking.Date, vehicleResponse.Id, userResponse.Id);
        // THEN
        this.SholdHaveCalledAskAsyncWithCorrectVehicleFinderQueryParametersOnce(vehicleFinderQuery);
    }

    [Fact]
    public async Task ShouldCallAskAsyncWithCorrectUserFinderQueryParameters()
    {
        // GIVEN
        VehicleResponse vehicleResponse = VehicleResponseMother.CreateRandom();
        UserResponse userResponse = UserResponseMother.CreateRandom();
        VehicleFinderQuery vehicleFinderQuery = VehicleFinderQuery.Create(vehicleResponse.Id);
        UserFinderQuery userFinderQuery = UserFinderQuery.Create(userResponse.Id);
        Booking booking = BookingMother.CreateRandom();
        this.ShouldReturnVehicleResponseThenUserResponse(vehicleFinderQuery, vehicleResponse, userFinderQuery, userResponse);
        // WHEN
        await _bookingCreator.Execute(booking.Id, booking.Date, vehicleResponse.Id, userResponse.Id);
        // THEN
        this.ShouldHaveCalledAskAsyncWithCorrectUserFinderQueryParametersOnce(userFinderQuery);
    }
    
    [Fact]
    public async Task ShouldCallSaveWithCorrectParameters()
    {
        // GIVEN
        VehicleResponse vehicleResponse = VehicleResponseMother.CreateRandom();
        UserResponse userResponse = UserResponseMother.CreateRandom();
        VehicleFinderQuery vehicleFinderQuery = VehicleFinderQuery.Create(vehicleResponse.Id);
        UserFinderQuery userFinderQuery = UserFinderQuery.Create(userResponse.Id);
        Booking booking = Booking.Create(
            BookingIdMother.CreateRandom(),
            BookingDateMother.CreateRandom(),
            vehicleResponse,
            userResponse
        );
        this.ShouldReturnVehicleResponseThenUserResponse(vehicleFinderQuery, vehicleResponse, userFinderQuery, userResponse);
        // WHEN
        await _bookingCreator.Execute(booking.Id, booking.Date, vehicleResponse.Id, userResponse.Id);
        // THEN
        this.ShouldHaveCalledSaveWithCorrectParametersOnce(booking);
    }

    [Fact]
    public async Task ShouldCallDispatchAsyncWithCorrectParameters()
    {
        // GIVEN
        VehicleResponse vehicleResponse = VehicleResponseMother.CreateRandom();
        UserResponse userResponse = UserResponseMother.CreateRandom();
        VehicleFinderQuery vehicleFinderQuery = VehicleFinderQuery.Create(vehicleResponse.Id);
        UserFinderQuery userFinderQuery = UserFinderQuery.Create(userResponse.Id);
        Booking booking = Booking.Create(
            BookingIdMother.CreateRandom(),
            BookingDateMother.CreateRandom(),
            vehicleResponse,
            userResponse
        );
        UserUpdaterCommand userUpdaterCommand = UserUpdaterCommand.Create(
            userResponse.Id,
            userResponse.Name,
            userResponse.Email,
            userResponse.State,
            vehicleResponse.Id);
        this.ShouldReturnVehicleResponseThenUserResponse(vehicleFinderQuery, vehicleResponse, userFinderQuery, userResponse);
        // WHEN
        await _bookingCreator.Execute(booking.Id, booking.Date, vehicleResponse.Id, userResponse.Id);
        // THEN
        this.ShouldHaveCalledDispatchAsyncWithCorrectParametersOnce(userUpdaterCommand);
    }
}