using FluentAssertions;
using Users.Shared.Bookings.Domain.Requests;

namespace UsersTests.Shared.Bookings.Domain.Requests;

public class BookingRequestCtrlTest
{
    [Fact]
    public void ShouldInicialitePropertiesCorrectly()
    {
        // GIVEN
        BookingRequestCtrl bookingRequestCtrl = BookingRequestMother.CreateRandom();
        // WHEN
        BookingRequestCtrl request = new BookingRequestCtrl(
            bookingRequestCtrl.Id,
            bookingRequestCtrl.Date,
            bookingRequestCtrl.VehicleId,
            bookingRequestCtrl.UserId);
        // THEN
        request.Date.Should().Be(bookingRequestCtrl.Date);
        request.Id.Should().Be(bookingRequestCtrl.Id);
        request.VehicleId.Should().Be(bookingRequestCtrl.VehicleId);
        request.UserId.Should().Be(bookingRequestCtrl.UserId);
    }
}