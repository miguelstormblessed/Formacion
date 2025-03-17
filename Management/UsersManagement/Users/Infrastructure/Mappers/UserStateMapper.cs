using System.Data;
using Dapper;
using UsersManagement.Users.Domain.ValueObject;

namespace UsersManagement.Users.Infrastructure.Mappers;

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