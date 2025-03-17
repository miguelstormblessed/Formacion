using FluentAssertions;
using UsersManagement.Senders.Domain.ValueObject;

namespace UsersTests.UsersManagement.Senders.Domain.ValueObject;

public class SendMessageTest
{
    [Fact]
    public void ShouldCreateAnInstanceWithCorrectProperties()
    {
        // GIVEN
        string message = "Hello World!";
        // WHEN
        SendMessage sendMessage = SendMessage.Create(message);
        // THEN
        sendMessage.Should().NotBeNull();
        sendMessage.Message.Should().Be(message);
        sendMessage.Should().BeOfType(typeof(SendMessage));
    }
    [Fact]
    public void ShouldBeEquivalents()
    {
        // GIVEN
        SendMessage sendMessage = SendMessageMother.CreateRandom();
        // WHEN
        SendMessage sendMessage1 = SendMessage.Create(sendMessage.Message);
        SendMessage sendMessage2 = SendMessage.Create(sendMessage.Message);
        // THEN
        sendMessage1.Should().BeEquivalentTo(sendMessage2);
    }
}