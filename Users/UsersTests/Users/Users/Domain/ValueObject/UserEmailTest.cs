using FluentAssertions;
using Users.Shared.Users.Domain.Exceptions;
using Users.Users.Domain.ValueObject;

namespace UsersTests.Users.Users.Domain.ValueObject;

public class UserEmailTest
{
    [Fact]
    public void ShouldCreateUserEmail()
    {
        // GIVEN
        UserEmail mail = UserEmailMother.CreateRandom();
        // WHEN
        UserEmail userEmail = UserEmail.Create(mail.Email);
        // THEN
        userEmail.Should().NotBeNull();
        userEmail.Email.Should().Be(mail.Email);
    }
    [Fact]
    public void ShouldBeEquivalents()
    {
        // GIVEN
        UserEmail email = UserEmailMother.CreateRandom();
        // WHEN
        UserEmail email2 = UserEmail.Create(email.Email);
        UserEmail email3 =  UserEmail.Create(email.Email);
        // THEN
        email2.Should().Be(email3);
    }

    [Fact]
    public void ShouldNotCreateUserMail_WhenInputEmailIsIncorrectFormat()
    {
        
        // GIVEN
        UserEmail mail = UserEmailMother.CreateRandom();
        mail.Email = mail.Email.Replace("@", "");
        // WHEN
        var action = () => UserEmail.Create(mail.Email);
        // THEN
        action.Should().Throw<InvalidEmailException>();
        
    }
}