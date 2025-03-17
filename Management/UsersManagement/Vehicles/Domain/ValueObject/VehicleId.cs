
using System.Text.RegularExpressions;
using UsersManagement.Shared.Users.Domain.Exceptions;

namespace UsersManagement.Vehicles.Domain.ValueObject;
public class VehicleId : IEquatable<VehicleId>
{
    private VehicleId(string vehicleId)
    {
        this.IsValidId(vehicleId);
        this.IdValue = vehicleId;
    }
    protected VehicleId(){}
    public string IdValue { get; private set; }

    public static VehicleId Create(string vehicleId)
    {
        return new VehicleId(vehicleId);
    }

    private void IsValidId(string id)
    {
        // Expresión regular para UUID
        string uuidPattern = @"^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}$";

        // Verificar si el id coincide con el patrón de UUID
        if (!Regex.IsMatch(id, uuidPattern))
        {
            throw new InvalidIdException();
        }
    }
    public bool Equals(VehicleId? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return IdValue == other.IdValue;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((VehicleId)obj);
    }

    public override int GetHashCode()
    {
        return IdValue.GetHashCode();
    }
}