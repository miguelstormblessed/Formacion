using UsersManagement.Shared.Vehicles.Domain.Exceptions;

namespace UsersManagement.Vehicles.Domain.ValueObject;

public class VehicleColor
{
    public enum ColorValue
    {
        Red = 0,
        Blue = 1,
        Green = 2,
        Yellow = 3,
        Black = 4,
        White = 5,
        Silver = 6
    }
    private VehicleColor(ColorValue value)
    {
        VehicleColorChecks(value);
        this.Value = value;
    }

    public ColorValue Value { get; private set; }

    public static VehicleColor CreateVehicleColor(ColorValue value)
    {
        return new VehicleColor(value);
    }

    private static void VehicleColorChecks(ColorValue value)
    {
        if (!Enum.IsDefined(typeof(ColorValue), value))
        {
            throw new InvalidColorException();
        }
    }
    protected VehicleColor(){}

    public override string? ToString()
    {
        return Enum.GetName(typeof(ColorValue), Value);
    }

    // Métodos Equals & HashCode
    public bool Equals(VehicleColor? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return this.Value == other.Value;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return this.Equals((VehicleColor)obj);
    }

    public override int GetHashCode()
    {
        return (int)this.Value;
    }
    
}