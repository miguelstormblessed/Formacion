using Cojali.Shared.Domain.Bus.Event;
using UsersManagement.Bookings.Domain;
using UsersManagement.Senders.Application;
using UsersManagement.Senders.Domain;
using UsersManagement.Senders.Domain.ValueObject;
using UsersManagement.Shared.Bookings.Domain.DomainEvents;
using UsersManagement.Users.Domain;
using UsersManagement.Vehicles.Domain;
using UsersTests.UsersManagement.Bookings.Domain;
using UsersTests.UsersManagement.Users.Domain;
using UsersTests.UsersManagement.Vehicles.Domain;

namespace UsersTests.UsersManagement.Senders.Application;

public class SendEmailOnBookingCreatedTest : SendersModuleAplicationUnitTestCase
{
    private readonly Sender _sender;
    private readonly SendEmailOnBookingCreated _sendEmailOnBookingCreated;
    public SendEmailOnBookingCreatedTest()
    {
        this._sender = new Sender(this.SendRepository.Object);
        this._sendEmailOnBookingCreated = new SendEmailOnBookingCreated(this._sender);
    }

    [Fact]
    public async Task ShouldSendEmail_WhenBookingCreatedEventIsPublished()
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
        Send send = Send.Create(
            SendTo.Create(user.Email.Email),
            SendSubject.Create("Booking Created"),
            SendMessage.Create("Booking has been created and confirmed"));
        // WHEN
        await this._sendEmailOnBookingCreated.OnAsync((DomainEvent)bookingCreated);
        // THEN
        this.ShouldHaveCalledSendWithCorrectParametersOnce(send);
    }
}