using System.Data;
using Dapper;
using UsersManagement.Vehicles.Domain.ValueObject;

namespace UsersManagement.Vehicles.Infrastructure.Mappers;

public class VehicleColorMapper : SqlMapper.TypeHandler<VehicleColor>
{
    public override void SetValue(IDbDataParameter parameter, VehicleColor? value)
    {
        parameter.Value = value?.Value;
    }

    public override VehicleColor? Parse(object value)
    {
        VehicleColor color = null;
        if (Enum.TryParse((string) value, out VehicleColor.ColorValue colorValue))
        {
            color = VehicleColor.CreateVehicleColor(colorValue);
            return color;
        }

        return color;
    }
}