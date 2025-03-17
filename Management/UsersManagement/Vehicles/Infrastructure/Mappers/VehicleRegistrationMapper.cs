using System.Data;
using Dapper;
using UsersManagement.Vehicles.Domain.ValueObject;

namespace UsersManagement.Vehicles.Infrastructure.Mappers;

public class VehicleRegistrationMapper : SqlMapper.TypeHandler<VehicleRegistration>
{
    public override void SetValue(IDbDataParameter parameter, VehicleRegistration? value)
    {
        parameter.Value = value.RegistrationValue;
    }

    public override VehicleRegistration? Parse(object value)
    {
        return VehicleRegistration.Create(value.ToString());
    }
}