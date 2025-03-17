using FluentAssertions;
using UsersManagement.Shared.Bookings.Domain.Requests;
using UsersManagement.Shared.Bookings.Domain.Responses;
using UsersTests.UsersManagement.Bookings.Domain.ValueObject;
using UsersTests.UsersManagement.Users.Domain.ValueObject;
using UsersTests.UsersManagement.Vehicles.Domain.ValueObject;

namespace UsersTests.UsersManagement.Shared.Bookings.Domain.Requests;

public class BookingRequestCtrlTest
{
    [Fact]
    public void ShouldInicialitePropertiesCorrectly()
    {
        // GIVEN
        string date = BookingDateMother.CreateRandom().DateValue;
        string id = Guid.NewGuid().ToString();
        string vehicleId = Guid.NewGuid().ToString();
        string userId = Guid.NewGuid().ToString();
        // WHEN
        BookingRequestCtrl request = new BookingRequestCtrl(id, date, vehicleId, userId);
        // THEN
        request.Date.Should().Be(date);
        request.Id.Should().Be(id);
        request.VehicleId.Should().Be(vehicleId);
        request.UserId.Should().Be(userId);
    }
}