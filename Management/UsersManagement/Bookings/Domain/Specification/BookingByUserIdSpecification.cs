using System.Linq.Expressions;
using Cojali.Shared.Domain.Specification;

namespace UsersManagement.Bookings.Domain.Specification;

public class BookingByUserIdSpecification : SpecificationBase<Booking>
{
    public readonly string _id;
    
    public BookingByUserIdSpecification(string id) => _id = id;
    
    protected override Expression<Func<Booking, bool>> CreateExpression()
        => booking => booking.UserResponse.Id == this._id;
}