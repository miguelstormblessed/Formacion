using Bogus;
using Users.Shared.Senders.Domain.Requests;

namespace UsersTests.Shared.Senders.Domain.Requests;

public class SenderRequestMother
{
    public static SenderRequest CreateRandom()
    {
        Faker faker = new Faker();
        return new SenderRequest(
            faker.Random.AlphaNumeric(10),
            faker.Random.AlphaNumeric(10),
            faker.Random.AlphaNumeric(10));
    }
}