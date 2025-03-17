using FluentAssertions;
using UsersManagement.Shared.Bookings.Domain.Responses;
using UsersTests.UsersManagement.Bookings.Domain.ValueObject;
using UsersTests.UsersManagement.Users.Domain.ValueObject;
using UsersTests.UsersManagement.Vehicles.Domain.ValueObject;

namespace UsersTests.UsersManagement.Shared.Bookings.Domain.Responses;

public class BookingResponseCtrlTest
{
   [Fact]
   public void ShouldInicialitePropertiesCorrectly()
   {
      // GIVEN
      string date = BookingDateMother.CreateRandom().DateValue;
      string number = VehicleRegistrationMother.CreateRandom().RegistrationValue;
      string name = UserNameMother.CreateRandom().Name;
      string email = UserEmailMother.CreateRandom().Email;
      // WHEN
      BookingResponseCtrl response = BookingResponseCtrl.Create(date, number, name, email);
      // THEN
      response.Date.Should().Be(date);
      response.VehicleRegistrationNumber.Should().Be(number);
      response.Name.Should().Be(name);
      response.Email.Should().Be(email);
   }
}