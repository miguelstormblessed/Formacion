using FluentAssertions;
using UsersManagement.Senders.Domain;

namespace UsersTests.UsersManagement.Senders.Domain;

public class SendTest
{
    [Fact]
    public void ShouldCreateWithCorrectProperties()
    {
        // GIVEN
        Send sendTest = SendMother.CreateRandom();
        // WHEN
        Send send = Send.Create(sendTest.To, sendTest.Subject, sendTest.Message);
        // THEN
        send.Should().NotBeNull();
        send.To.Should().Be(sendTest.To);
        send.Subject.Should().Be(sendTest.Subject);
        send.Message.Should().Be(sendTest.Message);
        send.Should().BeOfType(typeof(Send));
    }

    [Fact]
    public void ShouldBeEquivalents()
    {
        // GIVEN
        Send send1 = SendMother.CreateRandom();
        // WHEN
        Send send2 = Send.Create(send1.To, send1.Subject, send1.Message);
        Send send3 = Send.Create(send2.To, send2.Subject, send2.Message);
        // THEN
        send2.Should().BeEquivalentTo(send3);
    }
}