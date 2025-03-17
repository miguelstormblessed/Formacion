using Cojali.Shared.Domain.Bus.Event;
using FluentAssertions;
using UsersManagement.Bookings.Domain;
using UsersManagement.Shared.Bookings.Domain.DomainEvents;

namespace UsersTests.UsersManagement.Bookings.Domain;

public class BookingTest
{
    [Fact]
    public void ShouldInicialitePropertiesCorrectly()
    {
        // GIVEN
        Booking booking = BookingMother.CreateRandom();
        // WHEN
        Booking result = Booking.Create(
            booking.Id,
            booking.Date,
            booking.VehicleResponse, 
            booking.UserResponse);
        // THEN
        booking.Id.IdValue.Should().Be(result.Id.IdValue);
        booking.Date.DateValue.Should().Be(result.Date.DateValue);
        booking.VehicleResponse.Should().BeEquivalentTo(result.VehicleResponse);
        booking.UserResponse.Should().BeEquivalentTo(result.UserResponse);
    }

    [Fact]
    public void ShouldInicialiteCreateBookingMotherPropertiesCorrectly()
    {
        // GIVEN
        Booking booking = BookingMother.CreateRandom();
        // WHEN
        Booking result = Booking.CreateBookingMother(
            booking.Id,
            booking.Date,
            booking.VehicleResponse, 
            booking.UserResponse);
        // THEN
        booking.Id.IdValue.Should().Be(result.Id.IdValue);
        booking.Date.DateValue.Should().Be(result.Date.DateValue);
        booking.VehicleResponse.Should().BeEquivalentTo(result.VehicleResponse);
        booking.UserResponse.Should().BeEquivalentTo(result.UserResponse);
    }

    [Fact]
    public void ShouldRecordEventWhenBookingIsCreated()
    {
        // GIVEN
        Booking bookingTest = BookingMother.CreateRandom();
        // WHEN
        Booking booking = Booking.Create(
            bookingTest.Id,
            bookingTest.Date,
            bookingTest.VehicleResponse,
            bookingTest.UserResponse
        );
        DomainEvent[]? domainEvents = booking.PullDomainEvents();
        // THEN
        domainEvents.Should().ContainSingle();
        domainEvents.Should().Contain(e => e is BookingCreated);
    }

    [Fact]
    public void ShouldRecordEventWhenBookingIsDeleted()
    {
        // GIVEN
        Booking bookingTest = BookingMother.CreateRandom();
        // WHEN
        Booking booking = Booking.CreateBookingMother(
            bookingTest.Id,
            bookingTest.Date,
            bookingTest.VehicleResponse,
            bookingTest.UserResponse
        );
        booking.Delete();
        DomainEvent[]? domainEvents = booking.PullDomainEvents();
        // THEN
        domainEvents.Should().ContainSingle();
        domainEvents.Should().Contain(e => e is BookingDeleted);
    }

    [Fact]
    public void ShouldBeEquivalents()
    {
        // GIVEN
        Booking booking = BookingMother.CreateRandom();
        // WHEN
        Booking booking2 = Booking.Create(
            booking.Id,
            booking.Date,
            booking.VehicleResponse,
            booking.UserResponse);
        Booking booking3 = Booking.Create(
            booking.Id,
            booking.Date,
            booking.VehicleResponse,
            booking.UserResponse);
        // THEN
        booking2.Should().Be(booking3);
    }
}