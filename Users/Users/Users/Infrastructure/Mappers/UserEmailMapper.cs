using System.Data;
using Dapper;
using UsersManagement.Users.Domain.ValueObject;

namespace UsersManagement.Users.Infrastructure.Mappers;

public class UserEmailMapper : SqlMapper.TypeHandler<UserEmail>
{
    public override UserEmail Parse(object value)
    {
        return UserEmail.Create(value.ToString());
    }

    public override void SetValue(IDbDataParameter parameter, UserEmail value)
    {
        parameter.Value = value.Email;
    }
}