using Bogus;
using UsersManagement.Users.Domain.ValueObject;

namespace UsersTests.Users.Domain.ValueObject;

public static class UserEmailMother
{
    private static readonly Faker _faker = new Faker();

    public static UserEmail CreateRandom()
    {
        return UserEmail.Create(_faker.Internet.Email());
    }
}