using FluentAssertions;
using UsersManagement.Bookings.Domain.ValueObject;
using UsersManagement.Shared.Users.Domain.Exceptions;

namespace UsersTests.UsersManagement.Bookings.Domain.ValueObject;

public class BookingIdTest
{
    [Fact]
    public void ShouldInicialitePropertiesCorrectly()
    {
        // GIVEN
        string id = Guid.NewGuid().ToString();
        // WHEN
        BookingId bookingId = BookingId.Create(id);
        // THEN
        bookingId.IdValue.Should().Be(id);
    }

    [Fact]
    public void ShouldThrowInvalidIdException()
    {
        // GIVEN
        string id = Guid.NewGuid().ToString().Replace("-", ".");
        // THEN
        Func<BookingId> bookingId = () => BookingId.Create(id);
        // THEN
        bookingId.Should().Throw<InvalidIdException>();
    }

    [Fact]
    public void ShouldBeEquivalents()
    {
        // GIVEN
        BookingId bookingId = BookingIdMother.CreateRandom();
        // WHEN
        BookingId bookingId2 = BookingId.Create(bookingId.IdValue);
        BookingId bookingId3 = BookingId.Create(bookingId.IdValue);
        // THEN
        bookingId2.Should().Be(bookingId3);
    }
}