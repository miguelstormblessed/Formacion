/*namespace UsersTests.Shared.Bookings.Domain.Requests;

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
}*/