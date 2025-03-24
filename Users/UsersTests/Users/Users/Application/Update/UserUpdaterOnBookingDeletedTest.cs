/*using Cojali.Shared.Domain.Bus.Event;
using UsersManagement.Users.Application.Find;
using UsersManagement.Users.Application.Update;
using UsersManagement.Users.Domain;
using UsersTests.Users.Domain;

namespace UsersTests.Users.Application.Update;

public class UserUpdaterOnBookingDeletedTest : UserModuleApplicationUnitTestCase
{
    private readonly UserUpdater updater;
    private readonly UserFinder userFinder;
    private readonly UserUpdaterOnBookingDeleted UserUpdaterOnBookingDeleted;
    public UserUpdaterOnBookingDeletedTest()
    {
        this.userFinder = new UserFinder(this.UserRepository.Object);
        this.updater = new UserUpdater(this.UserRepository.Object,
            this.userFinder, this.EventBus.Object, this.QueryBus.Object);
        this.UserUpdaterOnBookingDeleted = new UserUpdaterOnBookingDeleted(
            this.updater, this.userFinder);
    }

    [Fact]
    public async Task ShouldCallUpdate_WhenBookingDeletedEventIsPublished()
    {
        // GIVEN
        Booking booking = BookingMother.CreateRandom();
        Vehicle vehicle = VehicleMother.CreateRandom();
        Usuario user = UserMother.CreateRandom();
        BookingDeleted bookingDeleted = BookingDeleted.Create(
            booking.Id.IdValue,
            booking.Date.DateValue,
            vehicle.Id.IdValue,
            user.Id.Id,
            user.Name.Name,
            user.Email.Email,
            vehicle.VehicleRegistration.RegistrationValue);
        this.ShouldFindUser(user.Id, user);
        this.ShouldFindVehicle(vehicle.Id, vehicle);
        // WHEN
        await UserUpdaterOnBookingDeleted.OnAsync((DomainEvent)bookingDeleted);
        // THEN
        this.ShouldHaveCalledUpdateWithCorrectParametersOnce(user);
    }
}*/