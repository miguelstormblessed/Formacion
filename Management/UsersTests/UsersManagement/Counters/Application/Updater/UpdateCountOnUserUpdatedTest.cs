using Cojali.Shared.Domain.Bus.Event;
using FluentAssertions;
using UsersManagement.Counters.Application.Finder;
using UsersManagement.Counters.Application.Updater;
using UsersManagement.Counters.Domain;
using UsersManagement.Shared.Users.Domain.DomainEvents;
using UsersManagement.Users.Domain;
using UsersManagement.Users.Domain.ValueObject;
using UsersTests.UsersManagement.Counters.Domain;
using UsersTests.UsersManagement.Users.Domain;

namespace UsersTests.UsersManagement.Counters.Application.Updater;

public class UpdateCountOnUserUpdatedTest : CountersModuleApplicationUnitTestCase
{
    private readonly UsersCounterUpdater _updateCounter;
    private readonly UsersCounterFinder _finder;
    private readonly UpdateCountOnUserUpdated _updateCountOnUserUpdated;
    public UpdateCountOnUserUpdatedTest()
    {
        this._finder = new UsersCounterFinder(this.CountersRepository.Object);
        this._updateCounter = new UsersCounterUpdater(this.CountersRepository.Object, this._finder);
        this._updateCountOnUserUpdated = new UpdateCountOnUserUpdated(this._updateCounter);
    }

    [Fact]
    public void ShouldCallUpdateOnce()
    {
        // GIVEN
        Usuario user = UserMother.CreateRandom();
        Usuario oldUser = UserMother.CreateRandom();
        UserUpdated userUpdatedDomainEvent = UserUpdated.Create(
            user.Id.Id,
            user.Name.Name,
            user.Email.Email,
            true,
            oldUser.Name.Name,
            oldUser.Email.Email,
            true
        );
        Count count = CountMother.CreateRandom();
        this.ShouldFindCount(count);
        this.ShouldUpdateCount(count);
        // WHEN
        this._updateCountOnUserUpdated.OnAsync((DomainEvent)userUpdatedDomainEvent);
        // THEN
        this.ShouldHaveCalledUpdateOnceWithCorrectParameters(count);
    }

    [Fact]
    public void ShouldUpdateCountOnUserUpdated_WhenUserUpdatedToInactive()
    {
        // GIVEN
        Usuario user = UserMother.CreateRandom();
        Usuario oldUser = UserMother.CreateRandom();
        UserUpdated userUpdatedDomainEvent = UserUpdated.Create(
            user.Id.Id,
            user.Name.Name,
            user.Email.Email,
            true,
            oldUser.Name.Name,
            oldUser.Email.Email,
            false
        );
        Count count = CountMother.CreateRandom();
        int oldActives = count.ActiveUsers;
        int oldInactives = count.InactiveUsers;
        this.ShouldFindCount(count);
        this.ShouldUpdateCount(count);
        // WHEN
        this._updateCountOnUserUpdated.OnAsync((DomainEvent)userUpdatedDomainEvent);
        // THEN
        count.ActiveUsers.Should().Be(oldActives - 1);
        count.InactiveUsers.Should().Be(oldInactives + 1);

    }
    
    [Fact]
    public void ShouldUpdateCountOnUserUpdated_WhenUserUpdatedToActive()
    {
        // GIVEN
        Usuario user = UserMother.CreateRandom();
        Usuario oldUser = UserMother.CreateRandom();
        UserUpdated userUpdatedDomainEvent = UserUpdated.Create(
            user.Id.Id,
            user.Name.Name,
            user.Email.Email,
            false,
            oldUser.Name.Name,
            oldUser.Email.Email,
            true
        );
        Count count = CountMother.CreateRandom();
        int oldActives = count.ActiveUsers;
        int oldInactives = count.InactiveUsers;
        this.ShouldFindCount(count);
        this.ShouldUpdateCount(count);
        // WHEN
        this._updateCountOnUserUpdated.OnAsync((DomainEvent)userUpdatedDomainEvent);
        // THEN
        count.ActiveUsers.Should().Be(oldActives + 1);
        count.InactiveUsers.Should().Be(oldInactives -1);

    }
}