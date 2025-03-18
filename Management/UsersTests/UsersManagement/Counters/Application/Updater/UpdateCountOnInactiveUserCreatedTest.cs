/*using Cojali.Shared.Domain.Bus.Event;
using FluentAssertions;
using UsersManagement.Counters.Application.Finder;
using UsersManagement.Counters.Application.Updater;
using UsersManagement.Counters.Domain;
using UsersManagement.Shared.Users.Domain.DomainEvents;
using UsersManagement.Users.Domain;
using UsersTests.UsersManagement.Counters.Domain;
using UsersTests.UsersManagement.Users.Domain;

namespace UsersTests.UsersManagement.Counters.Application.Updater;

public class UpdateCountOnInactiveUserCreatedTest : CountersModuleApplicationUnitTestCase
{
    private readonly UsersCounterUpdater _updateCounter;
    private readonly UsersCounterFinder _finder;
    private readonly UpdateCountOnInactiveUserCreated _updateCountOnInactivedUserCreated;
    public UpdateCountOnInactiveUserCreatedTest()
    {
        this._finder = new UsersCounterFinder(this.CountersRepository.Object);
        this._updateCounter = new UsersCounterUpdater(this.CountersRepository.Object, this._finder);
        this._updateCountOnInactivedUserCreated = new UpdateCountOnInactiveUserCreated(this._updateCounter);
    }

    [Fact]
    public void ShouldCallUpdateOnce()
    {
        // GIVEN
        Usuario user = UserMother.CreateRandom();
        InactiveUserCreated inactivedUserCreatedDomainEvent = InactiveUserCreated.Create(
            user.Id.Id,
            user.Name.Name,
            user.Email.Email,
            user.State.Active
            );
        Count count = CountMother.CreateRandom();
        this.ShouldFindCount(count);
        // WHEN
        this._updateCountOnInactivedUserCreated.OnAsync((DomainEvent)inactivedUserCreatedDomainEvent);
        // THEN
        this.ShouldHaveCalledUpdateOnceWithCorrectParameters(count);
    }

    [Fact]
    public void ShouldUpdateCountOnInactivedUserCreated()
    {
        // GIVEN
        Usuario user = UserMother.CreateRandom();
        InactiveUserCreated inactivedUserCreatedDomainEvent = InactiveUserCreated.Create(
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
        this._updateCountOnInactivedUserCreated.OnAsync((DomainEvent)inactivedUserCreatedDomainEvent);
        // THEN
        count.ActiveUsers.Should().Be(oldActives);
        count.InactiveUsers.Should().Be(oldInactives + 1);

    }
}*/