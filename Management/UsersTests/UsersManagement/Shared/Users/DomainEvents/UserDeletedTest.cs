using FluentAssertions;
using UsersManagement.Shared.Users.Domain.DomainEvents;
using UsersTests.UsersManagement.Users.Domain.ValueObject;

namespace UsersTests.UsersManagement.Shared.Users.DomainEvents;

public class UserDeletedTest
{
    [Fact]
    public void ShouldInicialitePropertiesCorrectly()
    {
        // GIVEN
        string identifier = Guid.NewGuid().ToString();
        string name = UserNameMother.CreateRandom().Name;
        string email = UserEmailMother.CreateRandom().Email;
        bool state = true;
        // WHEN
        UserDeleted userDeleted = UserDeleted.Create(identifier, name, email, state);
        // THEN
        userDeleted.Name.Should().Be(name);
        userDeleted.Email.Should().Be(email);
        userDeleted.State.Should().Be(state);
    }
}