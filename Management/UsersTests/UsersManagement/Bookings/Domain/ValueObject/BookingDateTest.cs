using FluentAssertions;
using UsersManagement.Bookings.Domain.ValueObject;
using UsersManagement.Shared.Bookings.Domain.Exceptions;

namespace UsersTests.UsersManagement.Bookings.Domain.ValueObject;

public class BookingDateTest
{
    [Fact]
    public void ShouldInicialiteWithCorrectProperties()
    {
        // GIVEN
        string date = BookingDateMother.CreateRandom().DateValue;
        // WHEN
        BookingDate bookingDate = BookingDate.Create(date);
        // THEN
        bookingDate.DateValue.Should().Be(date);
    }

    [Fact]
    public void ShouldThrowInvalidDateFormatException_WhenDateHasIncorrectFormat()
    {
        // GIVEN
        string date = "13-07-99";
        // WHEN
        Func<BookingDate> bookingDate = () => BookingDate.Create(date);
        // THEN
        bookingDate.Should().Throw<InvalidDateFormatException>();
    }

    [Fact]
    public void ShouldThrowInvalidDateFormatException_WhenIsADateInThePast()
    {
        // GIVEN
        string date = "13-07-1999";
        // WHEN
        var bookingDate = () => BookingDate.Create(date);
        // THEN
        bookingDate.Should().Throw<InvalidDateFormatException>();
    }

    [Fact]
    public void ShouldBeEquivalents()
    {
        // GIVEN
        BookingDate date = BookingDateMother.CreateRandom();
        // WHEN
        BookingDate bookingDate = BookingDate.Create(date.DateValue);
        BookingDate bookingDate2 = BookingDate.Create(date.DateValue);
        // THEN
        bookingDate.Should().Be(bookingDate2);

    }
}