using System.Text.Json;
using System.Text.Json.Serialization;
using FluentAssertions;
using Microsoft.AspNetCore.Authentication;
using UsersManagement.Vehicles.Domain;
using UsersManagement.Vehicles.Domain.ValueObject;
using UsersTests.UsersManagement.Vehicles.Domain.ValueObject;

namespace UsersTests.UsersManagement.Vehicles.Domain;

public class VehicleTest
{
    [Fact]
    public void ShouldInicialiteWithCorrectProperties()
    {
        // GIVEN
        VehicleId vehicleId = VehicleIdMother.CreateRandom();
        VehicleRegistration vehicleRegistration = VehicleRegistrationMother.CreateRandom();
        VehicleColor color = VehicleColorMother.CreateRandom();
        // WHEN
        Vehicle vehicle = Vehicle.Create(vehicleId, vehicleRegistration, color);
        // THEN
        vehicle.Should().NotBeNull();
        vehicle.Id.Should().Be(vehicleId);
        vehicle.VehicleRegistration.Should().Be(vehicleRegistration);
        vehicle.VehicleColor.Should().Be(color);
    }
    
}