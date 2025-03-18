/*using FluentAssertions;
using UsersManagement.Shared.Users.Domain.DomainEvents;
using UsersTests.UsersManagement.Users.Domain.ValueObject;

namespace UsersTests.UsersManagement.Shared.Users.DomainEvents;

public class InactiveUserCreatedTest
{
    [Fact]
    public void ShouldInicialitePropertiesCorrectly()
    {
        // GIVEN
        string identifier = Guid.NewGuid().ToString();
        string name = UserNameMother.CreateRandom().Name;
        string email = UserEmailMother.CreateRandom().Email;
        bool state = false;
        // WHEN
        InactiveUserCreated inactiveUserCreated = InactiveUserCreated.Create(identifier, name, email, state);
        // THEN
        inactiveUserCreated.Name.Should().Be(name);
        inactiveUserCreated.Email.Should().Be(email);
        inactiveUserCreated.State.Should().Be(state);
    }
}*/