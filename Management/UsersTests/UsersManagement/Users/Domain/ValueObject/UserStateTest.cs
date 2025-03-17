using FluentAssertions;
using UsersManagement.Users.Domain.ValueObject;

namespace UsersTests.UsersManagement.Users.Domain.ValueObject;

public class UserStateTest
{
    [Fact]
    public void ShouldCreateUserStateWithCorrectPropertiesAndType()
    {
        // GIVEN
        UserState state = UserStateMother.CreateRandom();
        // WHEN
        UserState userState = UserState.Create(state.Active);
        // THEN
        userState.Should().NotBeNull();
        userState.Should().BeOfType<UserState>();
        userState.Active.Should().Be(state.Active);
        
    }

    [Fact]
    public void ShouldBeEquivalents()
    {
        // GIVEN
        UserState state = UserStateMother.CreateRandom();
        // THEN
        UserState state2 = UserState.Create(state.Active);
        UserState state3 = UserState.Create(state.Active);
        // WHEN
        state2.Should().BeEquivalentTo(state3);
    }
}