using UsersManagement.Counters.Domain.ValueObject;

namespace UsersTests.UsersManagement.Counters.Domain.ValueObject;

public class CountIdMother
{
    public static CountId CreateRandom()
    {
        return CountId.Create(Guid.NewGuid().ToString());
    }
}