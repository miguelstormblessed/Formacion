using UsersManagement.Counters.Application.Finder;
using UsersManagement.Counters.Domain;
using UsersManagement.Shared.Counters.Domain.Exceptions;

namespace UsersManagement.Counters.Application.Updater;

public class UsersCounterUpdater
{
    private readonly ICountRepository _countRepository;
    private readonly UsersCounterFinder _finder;
    public UsersCounterUpdater(ICountRepository countRepository, UsersCounterFinder finder)
    {
        _countRepository = countRepository;
        _finder = finder;
    }

    public async Task Execute(int activeUsers, int inactiveUsers)
    {
        Count count = await this._finder.Execute();
        if (count == null)
        {
            throw new CountNotFoundException();
        }
        count.IncrementActiveUsers(activeUsers, inactiveUsers);
        count.Update(count.ActiveUsers,count.InactiveUsers);
        
        this._countRepository.Update(count);
    }
}