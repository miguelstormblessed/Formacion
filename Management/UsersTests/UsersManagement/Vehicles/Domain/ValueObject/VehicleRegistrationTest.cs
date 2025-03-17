using FluentAssertions;
using UsersManagement.Vehicles.Domain.ValueObject;

namespace UsersTests.UsersManagement.Vehicles.Domain.ValueObject;

public class VehicleRegistrationTest
{
    [Fact]
    public void ShouldInicialitePropertiesCorrectly()
    {
        // GIVEN
        VehicleRegistration vehicleRegistration = VehicleRegistrationMother.CreateRandom();
        // WHEN
        VehicleRegistration registration = VehicleRegistration.Create(vehicleRegistration.RegistrationValue);
        // THEN
        registration.RegistrationValue.Should().Be(vehicleRegistration.RegistrationValue);
    }

    [Fact]
    public void ShouldBeEquivalents()
    {
        // GIVEN
        VehicleRegistration vehicleRegistration = VehicleRegistrationMother.CreateRandom();
        // WHEN
        VehicleRegistration registration = VehicleRegistration.Create(vehicleRegistration.RegistrationValue);
        VehicleRegistration registration2 = VehicleRegistration.Create(vehicleRegistration.RegistrationValue);
        // THEN
        registration.Should().Be(registration2);
    }
}