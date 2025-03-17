using System.Data;
using Dapper;
using UsersManagement.Bookings.Domain.ValueObject;

namespace UsersManagement.Bookings.Infrastructure.Mappers;

public class BookingIdMapper : SqlMapper.TypeHandler<BookingId>
{
    public override void SetValue(IDbDataParameter parameter, BookingId? value)
    {
        parameter.Value = value;
    }

    public override BookingId? Parse(object value)
    {
        return BookingId.Create(value.ToString());
    }
}