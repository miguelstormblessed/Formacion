using System.Data;
using Dapper;
using UsersManagement.Users.Domain.ValueObject;

namespace UsersManagement.Users.Infrastructure.Mappers;

public class UserNameMapper : SqlMapper.TypeHandler<UserName>
{
    public override UserName Parse(object value)
    {
        return UserName.Create(value.ToString());
    }

    public override void SetValue(IDbDataParameter parameter, UserName value)
    {
        parameter.Value = value.Name;
    }}