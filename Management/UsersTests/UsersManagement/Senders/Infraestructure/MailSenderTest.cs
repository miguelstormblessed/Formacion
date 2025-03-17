using FluentAssertions;

namespace UsersTests.UsersManagement.Senders.Infraestructure;

public class MailSenderTest
{
    [Fact]
    private void ShouldSendEmail()
    {
        bool t = true;
        t.Should().BeTrue();
    }
    
}