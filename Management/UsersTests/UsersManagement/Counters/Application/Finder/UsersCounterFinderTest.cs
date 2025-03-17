using FluentAssertions;
using UsersManagement.Counters.Application.Finder;
using UsersManagement.Counters.Domain;
using UsersManagement.Shared.Counters.Domain.Exceptions;
using UsersTests.UsersManagement.Counters.Domain;

namespace UsersTests.UsersManagement.Counters.Application.Finder;

public class UsersCounterFinderTest : CountersModuleApplicationUnitTestCase
{
    private readonly UsersCounterFinder _finder;
    public UsersCounterFinderTest() 
    {
        _finder = new UsersCounterFinder(this.CountersRepository.Object);
    }

    [Fact]
    public void ShouldHaveCalledFindeOnce()
    {
        Count count = CountMother.CreateRandom();
        this.ShouldFindCount(count);
        // THEN
        this._finder.Execute();
        // WHEN
        this.ShouldHaveCalledFindeOnceWithCorrectParameters();
    }
    
    [Fact]
    public async Task ShouldReturnCount()
    {
        // GIVEN
        Count count = CountMother.CreateRandom();
        this.ShouldFindCount(count);
        // THEN
        Count result = await this._finder.Execute();
        // WHEN
        result.Should().Be(count);
    }

    [Fact]
    public async Task ShouldReturnCountNotfoundException_WhenCountIsNotFound()
    {
        // GIVEN
        
        // WHEN
        var result = async () => await this._finder.Execute();
        // THEN
        await result.Should().ThrowAsync<CountNotFoundException>();
    }
}