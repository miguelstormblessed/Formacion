using System.Runtime.CompilerServices;
using Bogus;
using UsersManagement.Shared.Users.Domain.Responses;

namespace UsersTests.UsersManagement.Shared.Users.Responses;

public class UserResponseMother
{
    private static readonly Faker _faker = new Faker();
    public static UserResponse CreateRandom()
    {
        return UserResponse.Create(
                Guid.NewGuid().ToString(),
                GetRandomName(),
                GetRandomEmail(),
                GetRandomActive()
                );
    }

    private static string GetRandomName()
    {
        return _faker.Random.AlphaNumeric(10);
    }

    private static string GetRandomEmail()
    {
        return _faker.Internet.Email();
    }

    private static bool GetRandomActive()
    {
        return _faker.Random.Bool();
    }
}