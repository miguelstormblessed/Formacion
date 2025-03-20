using System.Data;
using Dapper;
using Users.Users.Domain.ValueObject;

namespace Users.Users.Infrastructure.Mappers;

public class UserStateMapper : SqlMapper.TypeHandler<UserState>
{
    public override UserState Parse(object value)
    {
        return UserState.Create((bool)value);
    }

    public override void SetValue(IDbDataParameter parameter, UserState value)
    {
        parameter.Value = value.Active;
    }
}