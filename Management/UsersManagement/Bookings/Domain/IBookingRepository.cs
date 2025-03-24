using Cojali.Shared.Domain.Specification;
using UsersManagement.Bookings.Domain.ValueObject;
using UsersManagement.Shared.Users.Domain.Responses;

namespace UsersManagement.Bookings.Domain;

public interface IBookingRepository
{
    Task<IEnumerable<Booking>> SearchAll();

    Task<IEnumerable<Booking>> Search(SpecificationBase<Booking> genericSpecification);
    
    Booking GetBookingById(BookingId id);
    
    void Save(Booking booking);
    
    void Delete(BookingId bookingId);

    void Patch(BookingStatus bookingStatus, BookingId bookingId);


}