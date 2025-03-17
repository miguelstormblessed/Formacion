using System.Data;
using Dapper;
using UsersManagement.Bookings.Domain;
using UsersManagement.Bookings.Domain.ValueObject;


namespace UsersManagement.Bookings.Infrastructure.Mappers;

public class BookingDateMapper : SqlMapper.TypeHandler<BookingDate>
{
    public override void SetValue(IDbDataParameter parameter, BookingDate? value)
    {
        parameter.Value = value;
    }

    public override BookingDate? Parse(object value)
    {
        return BookingDate.Create(value.ToString());
    }
}