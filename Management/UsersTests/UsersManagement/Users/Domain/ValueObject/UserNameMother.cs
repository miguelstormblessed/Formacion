using Bogus;
using UsersManagement.Users.Domain.ValueObject;

namespace UsersTests.UsersManagement.Users.Domain.ValueObject;

public class UserNameMother
{
    private static readonly Faker _faker = new Faker();

    public static UserName CreateRandom()
    {
        return UserName.Create(_faker.Random.AlphaNumeric(10));
    }
}