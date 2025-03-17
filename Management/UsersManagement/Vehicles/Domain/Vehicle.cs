using System.Text.Json;
using System.Text.Json.Serialization;
using UsersManagement.Users.Infrastructure.Mappers;
using UsersManagement.Vehicles.Domain.ValueObject;
using UsersManagement.Vehicles.Infrastructure.Mappers;

namespace UsersManagement.Vehicles.Domain;

public class Vehicle : IEquatable<Vehicle>
{

    private Vehicle(VehicleId id, VehicleRegistration vehicleVehicleRegistration, VehicleColor vehicleColor)
    {
        Id = id;
        VehicleRegistration = vehicleVehicleRegistration;
        VehicleColor = vehicleColor;
    }
    protected Vehicle() { }
    
    public VehicleId Id { get; private set; }
    public VehicleRegistration VehicleRegistration { get; private set; }
    public VehicleColor VehicleColor  { get; private set; }

    public static Vehicle Create(VehicleId id, VehicleRegistration vehicleRegistration, VehicleColor colorValue)
    {
        return new Vehicle(id, vehicleRegistration, colorValue);
    }
    
    public bool Equals(Vehicle? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id.Equals(other.Id) && VehicleRegistration.Equals(other.VehicleRegistration) && VehicleColor.Equals(other.VehicleColor);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Vehicle)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, VehicleRegistration, VehicleColor);
    }
}