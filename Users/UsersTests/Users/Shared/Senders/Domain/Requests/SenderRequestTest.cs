using FluentAssertions;
using Users.Shared.Senders.Domain.Requests;

namespace UsersTests.Users.Shared.Senders.Domain.Requests;

public class SenderRequestTest
{
    [Fact]
    public void ShouldInicialitePropertiesCorrectly()
    {
        // GIVEN
        SenderRequest senderRequest = SenderRequestMother.CreateRandom();
        // WHEN
        SenderRequest request = new SenderRequest(senderRequest.SendTo, senderRequest.SendSubject, senderRequest.SendMessage);
        // THEN
        request.SendTo.Should().Be(senderRequest.SendTo);
        request.SendSubject.Should().Be(senderRequest.SendSubject);
        request.SendMessage.Should().Be(senderRequest.SendMessage);
    }
}