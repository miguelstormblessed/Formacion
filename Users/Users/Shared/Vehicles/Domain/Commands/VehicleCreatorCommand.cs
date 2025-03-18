using Cojali.Shared.Domain.Bus.Command;

namespace Users.Shared.Vehicles.Domain.Commands;

public class VehicleCreatorCommand : Command, IEquatable<VehicleCreatorCommand>
{
    private VehicleCreatorCommand(string vehicleId)
    {
        this.VehicleId = vehicleId;
        this.VehicleRegistrationNumber = "000-ABC";
        this.VehicleColor = "Red";
    }
    public string VehicleId { get; set; }
    public string VehicleRegistrationNumber { get; set; }
    public string VehicleColor { get; set; }

    public static VehicleCreatorCommand Create(string vehicleId)
    {
        return new VehicleCreatorCommand(vehicleId);
    }

    public bool Equals(VehicleCreatorCommand? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return VehicleId == other.VehicleId && VehicleRegistrationNumber == other.VehicleRegistrationNumber && VehicleColor == other.VehicleColor;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((VehicleCreatorCommand)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(VehicleId, VehicleRegistrationNumber, VehicleColor);
    }
}