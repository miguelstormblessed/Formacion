using FluentAssertions;
using Users.Shared.Users.Domain.Exceptions;
using Users.Users.Domain.ValueObject;

namespace UsersTests.Users.Users.Domain.ValueObject;

public class UserNameTest
{
    [Fact]
    public void ShouldCreateUserNameWithCorrectPropertiesAndType()
    {
        
        // GIVEN
        UserName userName = UserNameMother.CreateRandom();
        // WHEN
        UserName name = UserName.Create(userName.Name);
        // THEN
        name.Should().NotBeNull();
        name.Should().BeOfType<UserName>();
        name.Name.Should().Be(userName.Name);
    }
    [Fact]
    public void ShouldBeEquivalents()
    {
        // GIVEN
        UserName user = UserNameMother.CreateRandom();
        // THEN
        UserName name2 = UserName.Create(user.Name);
        UserName name3 = UserName.Create(user.Name);
        // WHEN
        name2.Should().BeEquivalentTo(name3);
    }

    [Fact]
    public void ShouldReturnException_WhenUserNameIsNotValid()
    {
        // GIVEN
        UserName user = UserNameMother.CreateRandom();
        user.Name = user.Name.Substring(0, 4);
        // WHEN
        var action = () => UserName.Create(user.Name);
        // THEN
        action.Should().Throw<InvalidUserNameException>();
    }
}