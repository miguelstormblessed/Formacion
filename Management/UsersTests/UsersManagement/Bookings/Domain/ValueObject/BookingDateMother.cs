using System.Runtime.CompilerServices;
using Bogus;
using UsersManagement.Bookings.Domain.ValueObject;

namespace UsersTests.UsersManagement.Bookings.Domain.ValueObject;

public class BookingDateMother
{
    public static BookingDate CreateRandom()
    {
        Faker faker = new Faker();
        return BookingDate.Create(faker.Date.Future().ToString("dd/MM/yyyy"));
    }
}