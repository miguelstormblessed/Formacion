using FluentAssertions;
using UsersManagement.Shared.Users.Domain.DomainEvents;
using UsersTests.UsersManagement.Users.Domain.ValueObject;

namespace UsersTests.UsersManagement.Shared.Users.DomainEvents;

public class UserUpdatedTest
{
    [Fact]
    public void ShouldInicialitePropertiesCorrectly()
    {
        // GIVEN
        string identifier = Guid.NewGuid().ToString();
        string name = UserNameMother.CreateRandom().Name;
        string email = UserEmailMother.CreateRandom().Email;
        bool state = true;
        bool oldState = false;
        string Oldname = UserNameMother.CreateRandom().Name;
        string Oldemail = UserEmailMother.CreateRandom().Email;
        // WHEN
        UserUpdated userUpdated = UserUpdated.Create(identifier, name, email, state, Oldname, Oldemail, oldState );
        // THEN
        userUpdated.Name.Should().Be(name);
        userUpdated.Email.Should().Be(email);
        userUpdated.State.Should().Be(state);
    }
}