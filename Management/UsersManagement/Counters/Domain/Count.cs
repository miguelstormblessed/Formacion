using UsersManagement.Counters.Domain.ValueObject;

namespace UsersManagement.Counters.Domain;

public class Count : IEquatable<Count>
{
    private Count(CountId id, int activeUsers, int inactiveUsers)
    {
        this.Id = id;
        this.ActiveUsers = activeUsers;
        this.InactiveUsers = inactiveUsers;
    }
    
    public static Count Create(CountId id, int active, int inactive)
    {
        return new Count(id, active, inactive);
    }
    protected Count(){}
    public CountId Id { get; set; }
    public int ActiveUsers { get; set; }
    public int InactiveUsers { get; set; }
    
    public void IncrementActiveUsers(int activeUsers, int inactiveUsers)
    {
        this.ActiveUsers += activeUsers;
        this.InactiveUsers += inactiveUsers;
    }

    public void Update(int newActiveUsers, int newInactiveUsers)
    {
        this.ActiveUsers = newActiveUsers;
        this.InactiveUsers = newInactiveUsers;
    }

    public bool Equals(Count? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id.Equals(other.Id) && ActiveUsers == other.ActiveUsers && InactiveUsers == other.InactiveUsers;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Count)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, ActiveUsers, InactiveUsers);
    }
}