using Bogus;
using UsersManagement.Senders.Domain.ValueObject;

namespace UsersTests.UsersManagement.Senders.Domain.ValueObject;

public class SendSubjectMother
{
    public static SendSubject CreateRandom()
    {
        Faker faker = new Faker();
        return SendSubject.Create(faker.Random.AlphaNumeric(10));
    }
}