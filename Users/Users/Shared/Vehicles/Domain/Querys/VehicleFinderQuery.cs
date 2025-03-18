using Cojali.Shared.Domain.Bus.Query;
using Users.Shared.Vehicles.Domain.Responses;

namespace Users.Shared.Vehicles.Domain.Querys;

public class VehicleFinderQuery : Query<VehicleResponse>, IEquatable<VehicleFinderQuery>
{
    private VehicleFinderQuery(string vehicleId)
    {
        this.VehicleId = vehicleId;
    }

    public string VehicleId { get; set; }

    public static VehicleFinderQuery Create(string vehicleId)
    {
        return new VehicleFinderQuery(vehicleId);
    }

    public bool Equals(VehicleFinderQuery? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return VehicleId == other.VehicleId;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((VehicleFinderQuery)obj);
    }

    public override int GetHashCode()
    {
        return VehicleId.GetHashCode();
    }
}