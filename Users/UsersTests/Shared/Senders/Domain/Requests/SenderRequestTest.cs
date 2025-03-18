/*using FluentAssertions;
using Users.Shared.Senders.Domain.Requests;

namespace UsersTests.Shared.Senders.Domain.Requests;

public class SenderRequestTest
{
    [Fact]
    public void ShouldInicialitePropertiesCorrectly()
    {
        // GIVEN
        string sendTo = SendToMother.CreateRandom().To;
        string sendSubject = SendSubjectMother.CreateRandom().Subject;
        string sendMessage = SendMessageMother.CreateRandom().Message;
        // WHEN
        SenderRequest request = new SenderRequest(sendTo, sendSubject, sendMessage);
        // THEN
        request.SendTo.Should().Be(sendTo);
        request.SendSubject.Should().Be(sendSubject);
        request.SendMessage.Should().Be(sendMessage);
    }
}*/