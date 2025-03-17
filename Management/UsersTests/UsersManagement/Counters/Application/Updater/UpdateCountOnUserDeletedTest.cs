using Cojali.Shared.Domain.Bus.Event;
using FluentAssertions;
using UsersManagement.Counters.Application.Finder;
using UsersManagement.Counters.Application.Updater;
using UsersManagement.Counters.Domain;
using UsersManagement.Shared.Users.Domain.DomainEvents;
using UsersManagement.Users.Domain;
using UsersTests.UsersManagement.Counters.Domain;
using UsersTests.UsersManagement.Users.Domain;

namespace UsersTests.UsersManagement.Counters.Application.Updater;

public class UpdateCountOnUserDeletedTest : CountersModuleApplicationUnitTestCase
{
    private readonly UsersCounterUpdater _updateCounter;
    private readonly UsersCounterFinder _finder;
    private readonly UpdateCountOnUserDeleted _updateCountOnUserDeleted;
    public UpdateCountOnUserDeletedTest()
    {
        this._finder = new UsersCounterFinder(this.CountersRepository.Object);
        this._updateCounter = new UsersCounterUpdater(this.CountersRepository.Object, this._finder);
        this._updateCountOnUserDeleted = new UpdateCountOnUserDeleted(this._updateCounter);
    }

    [Fact]
    public void ShouldCallUpdateOnce()
    {
        // GIVEN
        Usuario user = UserMother.CreateRandom();
        UserDeleted userDeletedDomainEvent = UserDeleted.Create(
            user.Id.Id,
            user.Name.Name,
            user.Email.Email,
            user.State.Active
            );
        Count count = CountMother.CreateRandom();
        this.ShouldFindCount(count);
        // WHEN
        this._updateCountOnUserDeleted.OnAsync((DomainEvent)userDeletedDomainEvent);
        // THEN
        this.ShouldHaveCalledUpdateOnceWithCorrectParameters(count);
    }

    [Fact]
    public void ShouldUpdateCountOnUserDeleted()
    {
        // GIVEN
        Usuario user = UserMother.CreateRandom();
        UserDeleted userDeletedDomainEvent = UserDeleted.Create(
            user.Id.Id,
            user.Name.Name,
            user.Email.Email,
            user.State.Active
        );
        Count count = CountMother.CreateRandom();
        int oldActives = count.ActiveUsers;
        int oldInactives = count.InactiveUsers;
        this.ShouldFindCount(count);
        this.ShouldUpdateCount(count);
        // WHEN
        this._updateCountOnUserDeleted.OnAsync((DomainEvent)userDeletedDomainEvent);
        // THEN
        count.ActiveUsers.Should().Be(oldActives - 1);
        count.InactiveUsers.Should().Be(oldInactives + 1);

    }
}