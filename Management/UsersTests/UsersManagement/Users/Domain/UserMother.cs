using Bogus;
using UsersManagement.Shared.Vehicles.Domain.Responses;
using UsersManagement.Users.Domain;
using UsersManagement.Users.Domain.ValueObject;
using UsersManagement.Vehicles.Domain;
using UsersTests.UsersManagement.Shared.Vehicles.Domain.Responses;
using UsersTests.UsersManagement.Vehicles.Domain;
using UsersTests.UsersManagement.Vehicles.Domain.ValueObject;

namespace UsersTests.UsersManagement.Users.Domain;

public static class UserMother
{
    private static readonly Faker Faker = new Faker();

    public static Usuario CreateRandom()
    {
        UserId userId = UserId.Create(CreateRandomId());
        UserName userName = UserName.Create(CreateRandomUsername());
        UserEmail userEmail = UserEmail.Create(CreateRandomEmail());
        VehicleResponse vehicle = VehicleResponseMother.CreateRandom();
        return Usuario.CreateActivatedUserMother(userId, userName, userEmail, vehicle);
    }
    
    private static string CreateRandomId()
    {
        return Guid.NewGuid().ToString();
    }

    private static string CreateRandomUsername()
    {
        return Faker.Random.AlphaNumeric(10);
    }

    private static string CreateRandomEmail()
    {
        return Faker.Random.AlphaNumeric(10)+"@gmail.com";
    }
}