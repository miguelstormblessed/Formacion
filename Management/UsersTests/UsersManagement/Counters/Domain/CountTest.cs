using FluentAssertions;
using UsersManagement.Counters.Domain;

namespace UsersTests.UsersManagement.Counters.Domain;

public class CountTest
{
    [Fact]
    public void ShouldInicialitePropertiesCorrectly()
    {
        // GIVEN
        Count count = CountMother.CreateRandom();
        // WHEN
        Count countToTest = Count.Create(count.Id, count.ActiveUsers, count.InactiveUsers);
        // THEN
        countToTest.Should().NotBeNull();
        countToTest.Id.Should().Be(count.Id);
        countToTest.ActiveUsers.Should().Be(count.ActiveUsers);
        countToTest.InactiveUsers.Should().Be(count.InactiveUsers);
    }

    [Fact]
    public void ShouldBeEquivalents()
    {
        // GIVEN
        Count count = CountMother.CreateRandom();
        // WHEN
        Count count2 = Count.Create(count.Id, count.ActiveUsers, count.InactiveUsers);
        Count count3 = Count.Create(count.Id, count.ActiveUsers, count.InactiveUsers);
        // THEN
        count2.Should().BeEquivalentTo(count3);
    }

    [Fact]
    public void ShouldUpdateCorrectly()
    {
        // GIVEN
        Count count = CountMother.CreateRandom();
        Count newCount = CountMother.CreateRandom();
        // WHEN
        count.Update(newCount.ActiveUsers, newCount.InactiveUsers);
        // THEN
        count.ActiveUsers.Should().Be(newCount.ActiveUsers);
        count.InactiveUsers.Should().Be(newCount.InactiveUsers);
    }
}