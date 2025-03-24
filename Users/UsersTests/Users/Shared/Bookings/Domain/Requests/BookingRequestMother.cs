using Bogus;
using Users.Shared.Bookings.Domain.Requests;

namespace UsersTests.Users.Shared.Bookings.Domain.Requests;

public class BookingRequestMother
{
    public static BookingRequestCtrl CreateRandom()
    {
        return new BookingRequestCtrl(
            Guid.NewGuid().ToString(),
            GetRandomDate(),
            Guid.NewGuid().ToString(),
            Guid.NewGuid().ToString());
    }

    private static string GetRandomDate()
    {
        Faker faker = new Faker();
        return faker.Date.Future().ToString("dd/MM/yyyy");
    }
}