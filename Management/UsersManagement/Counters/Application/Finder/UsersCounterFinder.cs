using UsersManagement.Counters.Domain;
using UsersManagement.Shared.Counters.Domain.Exceptions;

namespace UsersManagement.Counters.Application.Finder;

public class UsersCounterFinder
{
    private readonly ICountRepository _countRepository;
    private readonly string uuid = "6a853495-b793-4066-884e-b8ea4751ead8";
    public UsersCounterFinder(ICountRepository countRepository)
    {
        this._countRepository = countRepository;
    }

    public async Task< Count> Execute()
    {
        Count? count = await this._countRepository.Find(uuid);

        if (count == null)
        {
            throw new CountNotFoundException();
        }
        return count;
    }
}