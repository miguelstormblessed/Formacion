/*using FluentAssertions;
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
      string date = 
      string number = VehicleResponseMother.CreateRandom().VehicleRegistration;
      string name = UserResponseMother.CreateRandom().Name;
      string email = UserResponseMother.CreateRandom().Email;
      // WHEN
      BookingResponseCtrl response = BookingResponseCtrl.Create(date, number, name, email);
      // THEN
      response.Date.Should().Be(date);
      response.VehicleRegistrationNumber.Should().Be(number);
      response.Name.Should().Be(name);
      response.Email.Should().Be(email);
   }
}*/