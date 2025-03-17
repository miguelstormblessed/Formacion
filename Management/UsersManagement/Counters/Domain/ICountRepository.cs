namespace UsersManagement.Counters.Domain;

public interface ICountRepository
{
    public Task Update(Count count);
    public Task<Count?> Find(string id);
}