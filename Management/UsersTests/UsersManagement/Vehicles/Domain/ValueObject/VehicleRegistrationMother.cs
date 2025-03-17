using Bogus;
using UsersManagement.Vehicles.Domain.ValueObject;

namespace UsersTests.UsersManagement.Vehicles.Domain.ValueObject;

public class VehicleRegistrationMother
{
    public static VehicleRegistration CreateRandom()
    {
        Faker faker = new Faker();
        string alphanumeric = faker.Random.AlphaNumeric(10);
        string number = alphanumeric.Insert(5, "-");
        return VehicleRegistration.Create(number);
    }
}