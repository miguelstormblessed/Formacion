using Bogus;
using UsersManagement.Senders.Domain.ValueObject;

namespace UsersTests.UsersManagement.Senders.Domain.ValueObject;

public class SendToMother
{
    public static SendTo CreateRandom()
    {
        Faker faker = new Faker();
        return SendTo.Create(faker.Random.AlphaNumeric(10));
    }
}