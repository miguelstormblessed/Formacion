using System.Data;
using Dapper;
using UsersManagement.Counters.Domain.ValueObject;

namespace UsersManagement.Counters.Infrastucture.Mapper;

public class CountMapper : SqlMapper.TypeHandler<CountId>
{
    public override void SetValue(IDbDataParameter parameter, CountId? value)
    {
        parameter.Value = value?.IdValue;
    }

    public override CountId? Parse(object value)
    {
        return CountId.Create(value.ToString());
    }
}