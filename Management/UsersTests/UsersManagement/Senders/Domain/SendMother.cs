using UsersManagement.Senders.Domain;
using UsersTests.UsersManagement.Senders.Domain.ValueObject;

namespace UsersTests.UsersManagement.Senders.Domain;

public class SendMother
{
    public static Send CreateRandom()
    {
        return Send.Create(
            SendToMother.CreateRandom(),
            SendSubjectMother.CreateRandom(),
            SendMessageMother.CreateRandom()
            );
    }
}