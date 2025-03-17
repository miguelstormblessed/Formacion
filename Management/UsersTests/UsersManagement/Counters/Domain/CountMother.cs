using UsersManagement.Counters.Domain;
using UsersTests.UsersManagement.Counters.Domain.ValueObject;

namespace UsersTests.UsersManagement.Counters.Domain;

public class CountMother
{
    public static Count CreateRandom()
    {
        Random random = new Random();
        return Count.Create(CountIdMother.CreateRandom(),random.Next(int.MaxValue),random.Next(int.MaxValue));
    }
}