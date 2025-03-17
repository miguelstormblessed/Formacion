using System.Text.Json.Serialization;
using UsersManagement.Shared.Vehicles.Domain.Exceptions;

namespace UsersManagement.Vehicles.Domain.ValueObject;
public class VehicleRegistration : IEquatable<VehicleRegistration>
{
    private VehicleRegistration(string registration)
    {
        this.IsValidVehicleRegistrationNumber(registration);
        this.RegistrationValue = registration;
    }
    protected VehicleRegistration(){}
    public string RegistrationValue {get; set;}

    public static VehicleRegistration Create(string registration)
    {
        return new VehicleRegistration(registration);
    }

    private void IsValidVehicleRegistrationNumber(string registrationNumber)
    {
        if (!registrationNumber.Contains('-'))
        {
            throw new InvalidVehicleRegistrationNumberException();
        }
    }
    public bool Equals(VehicleRegistration? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return RegistrationValue == other.RegistrationValue;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((VehicleRegistration)obj);
    }

    public override int GetHashCode()
    {
        return RegistrationValue.GetHashCode();
    }
}