using System.Data;
using Dapper;
using UsersManagement.Vehicles.Domain.ValueObject;

namespace UsersManagement.Vehicles.Infrastructure.Mappers;

public class VehicleIdMapper :  SqlMapper.TypeHandler<VehicleId>
{
    public override void SetValue(IDbDataParameter parameter, VehicleId? value)
    {
        parameter.Value = value?.IdValue;
    }

    public override VehicleId? Parse(object value)
    {
        return VehicleId.Create(value.ToString());
    }
}