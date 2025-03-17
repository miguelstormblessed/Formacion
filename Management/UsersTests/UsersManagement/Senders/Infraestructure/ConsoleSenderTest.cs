using FluentAssertions;
using UsersManagement.Senders.Domain;
using UsersManagement.Senders.Infraestructure;
using UsersTests.UsersManagement.Senders.Domain;

namespace UsersTests.UsersManagement.Senders.Infraestructure;

public class ConsoleSenderTest
{
    private readonly ConsoleSender _consoleSender;
    private readonly StringWriter _stringWriter;
    public ConsoleSenderTest()
    {
        _consoleSender = new ConsoleSender();
        _stringWriter = new StringWriter();
    }
    [Fact]
    public void ShouldWriteToConsole_WhenSending()
    {
        // GIVEN
        Send send = SendMother.CreateRandom();
        Console.SetOut(this._stringWriter);
        // WHEN
        _consoleSender.Send(send);
        string output = this._stringWriter.ToString().Trim();
        // THEN
        output.Should().Be(send.Message.Message);
    }
}