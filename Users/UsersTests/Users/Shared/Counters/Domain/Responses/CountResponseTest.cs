using FluentAssertions;
using Users.Shared.Counters.Domain.Responses;

namespace UsersTests.Users.Shared.Counters.Domain.Responses;

public class CountResponseTest
{
    [Fact]
    public void ShouldInicialitePropertiesCorrectly()
    {
        // GIVEN
        Random random = new Random();
        int activeUsers = random.Next(1, 10);
        int inactiveUsers = random.Next(1, 10);
        int totalUsers = activeUsers + inactiveUsers;
        // WHEN
        CountResponse response = new CountResponse(activeUsers, inactiveUsers);
        // THEN
        response.ActiveUsers.Should().Be(activeUsers);
        response.InactiveUsers.Should().Be(inactiveUsers);
        response.TotalUsers.Should().Be(totalUsers);
    }
}