using Bogus;
using UsersManagement.Senders.Domain.ValueObject;

namespace UsersTests.UsersManagement.Senders.Domain.ValueObject;

public class SendMessageMother
{
    public static SendMessage CreateRandom()
    {
        Faker faker = new Faker();
        return SendMessage.Create(faker.Random.AlphaNumeric(20));
    }
}