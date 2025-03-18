using Bogus;
using Users.Shared.Vehicles.Domain.Responses;

namespace UsersTests.Shared.Vehicles.Domain.Responses;

public class VehicleResponseMother
{
    private static readonly Faker _faker = new Faker();
    public static VehicleResponse CreateRandom()
    {
        return VehicleResponse.Create(
            Guid.NewGuid().ToString(),
            GetRandomVehicleRegistrationNumber(),
            GetRandomVehicleColor()
        );
    }

    private static string GetRandomVehicleRegistrationNumber()
    {
        string alphanumeric = _faker.Random.AlphaNumeric(10);
        return alphanumeric.Insert(5, "-");
    }

    private static string GetRandomVehicleColor()
    {
        string [] colors = new []{"Red", "Green", "Blue", "Yellow", "White", "Black", "Silver"};
        Random random = new Random();
        return colors[random.Next(colors.Length)];
    }
}