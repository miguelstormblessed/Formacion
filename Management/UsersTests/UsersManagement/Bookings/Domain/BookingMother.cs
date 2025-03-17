using UsersManagement.Bookings.Domain;
using UsersManagement.Bookings.Domain.ValueObject;
using UsersManagement.Shared.Users.Domain.Responses;
using UsersTests.UsersManagement.Bookings.Domain.ValueObject;
using UsersTests.UsersManagement.Shared.Users.Responses;
using UsersTests.UsersManagement.Shared.Vehicles.Domain.Responses;

namespace UsersTests.UsersManagement.Bookings.Domain;

public class BookingMother
{
    public static Booking CreateRandom()
    {
        return Booking.CreateBookingMother(
            BookingIdMother.CreateRandom(),
            BookingDateMother.CreateRandom(),
            VehicleResponseMother.CreateRandom(),
            UserResponseMother.CreateRandom()
            );
    }
}