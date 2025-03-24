using Bogus;
using Users.Users.Domain.ValueObject;

namespace UsersTests.Users.Users.Domain.ValueObject;

public class UserStateMother
{
    public static readonly Faker _faker = new Faker();

    public static UserState CreateRandom()
    {
         return UserState.Create(_faker.Random.Bool());
    }
   
}