using FluentAssertions;
using UsersManagement.Senders.Domain.ValueObject;

namespace UsersTests.UsersManagement.Senders.Domain.ValueObject;

public class SendSubjectTest
{
    [Fact]
    public void ShouldCreateWithCorrectProperties()
    {
        // GIVEN
        string subject = "Hello";
        // WHEN
        SendSubject sendSubject = SendSubject.Create(subject);
        // THEN
        sendSubject.Should().NotBeNull();
        sendSubject.Subject.Should().Be(subject);
        sendSubject.Should().BeOfType<SendSubject>();
    }

    [Fact]
    public void ShouldBeEquivalentes()
    {
        // GIVEN 
        SendSubject subject1 = SendSubjectMother.CreateRandom();
        // WHEN
        SendSubject subject2 = SendSubject.Create(subject1.Subject);
        SendSubject subject3 = SendSubject.Create(subject1.Subject);
        // THEN
        subject2.Should().BeEquivalentTo(subject3);
    }
}