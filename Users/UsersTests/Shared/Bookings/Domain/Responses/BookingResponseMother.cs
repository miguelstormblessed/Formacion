using System.Runtime.CompilerServices;
using Bogus;
using Users.Shared.Bookings.Domain.Responses;

namespace UsersTests.Shared.Bookings.Domain.Responses;

public class BookingResponseMother
{
    public static BookingResponseCtrl CreateRandom()
    {
        return BookingResponseCtrl.Create(
            GetRandomDate(),
            GetRandomVehicleRegistrationNumber(),
            GetRandomName(),
            GetRandomEmail());
    }

    public static string GetRandomDate()
    {
        Faker faker = new Faker();
        return faker.Date.Future().ToString("dd/MM/yyyy");
    }

    public static string GetRandomVehicleRegistrationNumber()
    {
        Faker faker = new Faker();
        string alphanumeric = faker.Random.AlphaNumeric(10);
        return alphanumeric.Insert(5, "-");
    }

    public static string GetRandomName()
    {
        Faker faker = new Faker();
        return faker.Random.AlphaNumeric(10);
    }

    public static string GetRandomEmail()
    {
        Faker faker = new Faker();
        return faker.Internet.Email();
    }
}