using System.Data;
using Dapper;
using Users.Users.Domain.ValueObject;

namespace Users.Users.Infrastructure.Mappers;

public class UserIdMapper : SqlMapper.TypeHandler<UserId>
{
    public override UserId Parse(object value)
    {
        return UserId.Create(value.ToString());
    }

    public override void SetValue(IDbDataParameter parameter, UserId value)
    {
        parameter.Value = value.Id;
    }
}