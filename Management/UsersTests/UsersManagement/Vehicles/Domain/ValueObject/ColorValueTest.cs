using FluentAssertions;
using UsersManagement.Vehicles.Domain.ValueObject;

namespace UsersTests.UsersManagement.Vehicles.Domain.ValueObject;

public class ColorValueTest
{
    [Fact]
    public void ShouldInicialitePropertiesCorrectly()
    {
        // GIVEN
        Array colors = Enum.GetValues<VehicleColor.ColorValue>();
        Random random = new Random();
        var color =(VehicleColor.ColorValue) colors.GetValue(random.Next(0, colors.Length));
        // WHEN
        VehicleColor vehicleColor = VehicleColor.CreateVehicleColor(color);
        // THEN
        vehicleColor.Value.Should().Be(color);
    }

    [Fact]
    public void ShouldBeEquivalents()
    {
        // GIVEN
        VehicleColor vehicleColor = VehicleColorMother.CreateRandom();
        // WHEN
        VehicleColor vehicleColor2 = VehicleColor.CreateVehicleColor(vehicleColor.Value);
        VehicleColor vehicleColor3 = VehicleColor.CreateVehicleColor(vehicleColor.Value);
        // THEN
        vehicleColor2.Should().BeEquivalentTo(vehicleColor3);
    }
}