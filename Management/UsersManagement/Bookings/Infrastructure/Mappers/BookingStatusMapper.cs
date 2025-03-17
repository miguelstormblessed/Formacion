using System.Data;
using Dapper;
using UsersManagement.Bookings.Domain.ValueObject;

namespace UsersManagement.Bookings.Infrastructure.Mappers;

public class BookingStatusMapper : SqlMapper.TypeHandler<BookingStatus>
{
    public override void SetValue(IDbDataParameter parameter, BookingStatus? value)
    {
        parameter.Value = value;
    }

    public override BookingStatus? Parse(object value)
    {
        return BookingStatus.Create((bool) value);
    }
}