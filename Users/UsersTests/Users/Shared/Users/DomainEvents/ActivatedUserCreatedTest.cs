/*using FluentAssertions;
using UsersManagement.Shared.Users.Domain.DomainEvents;
using UsersTests.UsersManagement.Users.Domain.ValueObject;

namespace UsersTests.UsersManagement.Shared.Users.DomainEvents;

public class ActivatedUserCreatedTest
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
        ActivatedUserCreated activatedUserCreated = ActivatedUserCreated.Create(identifier, name, email, state);
        // THEN
        activatedUserCreated.Name.Should().Be(name);
        activatedUserCreated.Email.Should().Be(email);
        activatedUserCreated.State.Should().Be(state);
    }
}*/