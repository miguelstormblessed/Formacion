using FluentAssertions;
using UsersManagement.Bookings.Domain.ValueObject;

namespace UsersTests.UsersManagement.Bookings.Domain.ValueObject;

public class BookingStatusTest
{
    [Fact]
    public void ShouldInicialiteWithCorrectProperties()
    {
        // GIVEN
        
        // WHEN
        BookingStatus status = BookingStatus.Create();
        // THEN
        status.StatusValue.Should().Be(true);
    }

    [Fact]
    public void ShouldBeEquivalents()
    {
        // GIVEN
        BookingStatus status = BookingStatus.Create();
        BookingStatus status2 = BookingStatus.Create();
        // WHEN
        // THEN
        status.Should().Be(status2);    
    }
}