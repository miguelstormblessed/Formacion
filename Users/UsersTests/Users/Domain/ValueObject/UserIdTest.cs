using FluentAssertions;
using Users.Shared.Users.Domain.Exceptions;
using UsersManagement.Users.Domain.ValueObject;

namespace UsersTests.Users.Domain.ValueObject;

public class UserIdTest
{
    [Fact]
    public void ShouldCreateUserIdWithCorrectProperties()
    {
        // GIVEN
        UserId id = UserIdMother.CreateRandom();
        // WHEN
        UserId userId = UserId.Create(id.Id);
        // THEN
        userId.Should().NotBeNull();
        userId.Id.Should().Be(id.Id);
    }
    [Fact]
    public void ShouldBeEquivalents()
    {
        // GIVEN
        UserId id = UserIdMother.CreateRandom();
        // WHEN
        UserId id2 = UserId.Create(id.Id);
        UserId id3 =  UserId.Create(id.Id);
        // THEN
        id2.Should().Be(id3);
    }

    [Fact]
    public void ShouldReturnException_WhenInputIdIsIncorrectFormat()
    {
        
        // GIVEN
        UserId userId = UserIdMother.CreateRandom();
        string uuidWithInvalidFormat = userId.Id.Replace("-", "");
        // WHEN
        
        var id = () => UserId.Create(uuidWithInvalidFormat);
        // THEN
        id.Should().Throw<InvalidIdException>();
        
    }
}