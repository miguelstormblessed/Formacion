using System.Linq.Expressions;
using Cojali.Shared.Domain.Specification;

namespace UsersManagement.Bookings.Domain.Specification;

public class BookingByVehicleIdSpecification : SpecificationBase<Booking>
{
    public readonly string _id;
    
    public BookingByVehicleIdSpecification(string id) => this._id = id;
    
    protected override Expression<Func<Booking, bool>> CreateExpression()
        => booking => booking.VehicleResponse.Id == this._id;
}