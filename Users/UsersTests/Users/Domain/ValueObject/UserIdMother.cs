using Bogus;
using Users.Users.Domain.ValueObject;

namespace UsersTests.Users.Domain.ValueObject;

public static class UserIdMother
{
    private static readonly Faker Faker = new Faker();
    public static UserId CreateRandom()
    {
        return UserId.Create(Guid.NewGuid().ToString());
    }

    public static UserId CreateRandomNotValid()
    {
        return UserId.Create(Faker.Random.AlphaNumeric(20));
    }
}