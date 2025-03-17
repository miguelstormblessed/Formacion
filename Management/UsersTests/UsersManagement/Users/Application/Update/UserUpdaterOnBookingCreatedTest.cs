using Cojali.Shared.Domain.Bus.Event;
using UsersManagement.Bookings.Domain;
using UsersManagement.Shared.Bookings.Domain.DomainEvents;
using UsersManagement.Shared.Users.Domain.Responses;
using UsersManagement.Shared.Vehicles.Domain.Responses;
using UsersManagement.Users.Application.Find;
using UsersManagement.Users.Application.Update;
using UsersManagement.Users.Domain;
using UsersManagement.Vehicles.Domain;
using UsersTests.UsersManagement.Bookings.Domain;
using UsersTests.UsersManagement.Shared.Users.Responses;
using UsersTests.UsersManagement.Shared.Vehicles.Domain.Responses;
using UsersTests.UsersManagement.Users.Domain;
using UsersTests.UsersManagement.Vehicles.Domain;

namespace UsersTests.UsersManagement.Users.Application.Update;

public class UserUpdaterOnBookingCreatedTest : UserModuleApplicationUnitTestCase
{
    private readonly UserUpdaterOnBookingCreated userUpdaterOnBookingCreated;
    private readonly UserUpdater updater;
    private readonly UserFinder userFinder;

    public UserUpdaterOnBookingCreatedTest()
    {
        userFinder = new UserFinder(this.UserRepository.Object);
        this.updater = new UserUpdater(this.UserRepository.Object,
            this.userFinder, this.EventBus.Object, this.QueryBus.Object);
        this.userUpdaterOnBookingCreated = new UserUpdaterOnBookingCreated(this.updater, this.userFinder);
    }

    [Fact]
    public async Task ShouldUpdateUser_WhenBookingCreatedEventIsPublished()
    {
        // GIVEN
        Booking booking = BookingMother.CreateRandom();
        Vehicle vehicle = VehicleMother.CreateRandom();
        Usuario user = UserMother.CreateRandom();
        BookingCreated bookingCreated = BookingCreated.Create(
            booking.Id.IdValue,
            booking.Date.DateValue,
            vehicle.Id.IdValue,
            user.Id.Id,
            user.Email.Email,
            vehicle.VehicleRegistration.RegistrationValue,
            user.Name.Name);
        this.ShouldFindUser(user.Id, user);
        this.ShouldFindVehicle(vehicle.Id, vehicle);
        // WHEN
        await userUpdaterOnBookingCreated.OnAsync((DomainEvent) bookingCreated);
        // THEN
        this.ShouldHaveCalledUpdateWithCorrectParametersOnce(user);
    }
}