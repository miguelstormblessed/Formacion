using FluentAssertions;
using UsersManagement.Senders.Domain.ValueObject;

namespace UsersTests.UsersManagement.Senders.Domain.ValueObject;

public class SendToTest
{
    [Fact]
    public void ShouldCreateWithCorrectProperties()
    {
        // GIVEN
        string email = "test@email.com";
        
        // WHEN
        SendTo sendTo = SendTo.Create(email);
        
        // THEN
        sendTo.Should().NotBeNull();
        sendTo.To.Should().Be(email);
        sendTo.Should().BeOfType<SendTo>();
    }

    [Fact]
    public void ShouldBeEquivalents()
    {
        // GIVEN
        SendTo sendTo = SendToMother.CreateRandom();
        // THEN
        SendTo sendTo2 = SendTo.Create(sendTo.To);
        SendTo sendTo3 = SendTo.Create(sendTo.To);
        // THEN
        sendTo2.Should().BeEquivalentTo(sendTo3);
    }
}