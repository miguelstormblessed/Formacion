using FluentAssertions;
using Users.Shared.Bookings.Domain.Requests;
using Users.Shared.Bookings.Domain.Responses;
using UsersTests.Shared.Users.Responses;
using UsersTests.Shared.Vehicles.Domain.Responses;

namespace UsersTests.Shared.Bookings.Domain.Responses;

public class BookingResponseCtrlTest
{
   [Fact]
   public void ShouldInicialitePropertiesCorrectly()
   {
      // GIVEN
      BookingResponseCtrl bookingResponseCtrl = BookingResponseMother.CreateRandom();
      // WHEN
      BookingResponseCtrl response = BookingResponseCtrl.Create(
         bookingResponseCtrl.Date, 
         bookingResponseCtrl.VehicleRegistrationNumber, 
         bookingResponseCtrl.Name, 
         bookingResponseCtrl.Email);
      // THEN
      response.Date.Should().Be(bookingResponseCtrl.Date);
      response.VehicleRegistrationNumber.Should().Be(bookingResponseCtrl.VehicleRegistrationNumber);
      response.Name.Should().Be(bookingResponseCtrl.Name);
      response.Email.Should().Be(bookingResponseCtrl.Email);
   }
}