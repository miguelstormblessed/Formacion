using FluentAssertions;
using Users.Shared.Vehicles.Domain.Commands;

namespace UsersTests.Shared.Vehicles.Domain.Commands;

public class VehicleCreatorCommandTest
{
    [Fact]
    public void ShouldInicialiteWithCorrectProperties()
    {
        // GIVEN
        string id = Guid.NewGuid().ToString();
        // WHEN
        VehicleCreatorCommand creatorCommand = VehicleCreatorCommand.Create(id);
        // THEN
        creatorCommand.VehicleId.Should().Be(id);
        creatorCommand.VehicleRegistrationNumber.Should().Be("000-ABC");
        creatorCommand.VehicleColor.Should().Be("Red");
    }

    [Fact]
    public void ShouldBeEquivalets()
    {
        // GIVEN
        VehicleCreatorCommand command1 = VehicleCreatorCommand.Create(Guid.NewGuid().ToString());
        // WHEN
        VehicleCreatorCommand command2 = VehicleCreatorCommand.Create(command1.VehicleId);
        VehicleCreatorCommand command3 = VehicleCreatorCommand.Create(command1.VehicleId);
        // THEN
        command2.Should().BeEquivalentTo(command3);
    }
}