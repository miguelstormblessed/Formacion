using FluentAssertions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using UsersManagement.Counters.Application.Finder;
using UsersManagement.Counters.Application.Updater;
using UsersManagement.Counters.Domain;
using UsersManagement.Shared.Counters.Domain.Exceptions;
using UsersTests.UsersManagement.Counters.Domain;

namespace UsersTests.UsersManagement.Counters.Application.Updater;

public class UsersCounterUpdaterTest : CountersModuleApplicationUnitTestCase
{
    private readonly UsersCounterFinder _finder;
    private readonly UsersCounterUpdater _updater;

    public UsersCounterUpdaterTest()
    {
        this._finder = new UsersCounterFinder(this.CountersRepository.Object);
        _updater = new UsersCounterUpdater(this.CountersRepository.Object, this._finder);
    }

    [Fact]
    public void ShouldCallUpdateOnce()
    {
        // GIVEN
        Count count = CountMother.CreateRandom();
        this.ShouldFindCount(count);
        this.ShouldUpdateCount(count);
        // WHEN
        this._updater.Execute(1,1);
        // THEN
        this.ShouldHaveCalledUpdateOnceWithCorrectParameters(count);
    }
    
    [Fact]
    public void ShouldUpdateUsersCounter()
    {
        // GIVEN
        Count count = CountMother.CreateRandom();
        int oldActives = count.ActiveUsers;
        int oldInactives = count.InactiveUsers;
        this.ShouldUpdateCount(count);
        this.ShouldFindCount(count);
        // WHEN
        this._updater.Execute(1,1);
        // THEN
        count.ActiveUsers.Should().Be(oldActives + 1);
        count.InactiveUsers.Should().Be(oldInactives + 1);
    }

    [Fact]
    public void ShouldThrowCountNotFountException()
    {
        // GIVEN
        // WHEN
        var result = () => this._updater.Execute(1,1);
        // THEN
        result.Should().ThrowAsync<CountNotFoundException>();
    }
}