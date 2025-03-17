using FluentAssertions;
using UsersManagement.Counters.Domain.ValueObject;

namespace UsersTests.UsersManagement.Counters.Domain.ValueObject;

public class CountIdTest
{
    [Fact]
    public void ShouldInicialitePropertiesCorrectly()
    {
        // GIVEN
        CountId id = CountIdMother.CreateRandom();
        // WHEN
        CountId newId = CountId.Create(id.IdValue);
        // THEN
        newId.Should().NotBeNull();
        newId.IdValue.Should().Be(id.IdValue);
    }

    [Fact]
    public void ShouldBeEquivalents()
    {
        // GIVEN
        CountId id = CountIdMother.CreateRandom();
        // WHEN
        CountId id2 = CountId.Create(id.IdValue);
        CountId id3 = CountId.Create(id.IdValue);
        // THEN
        id2.Should().BeEquivalentTo(id3);
    }
}