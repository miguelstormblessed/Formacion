namespace UsersManagement.Counters.Domain.ValueObject;

public class CountId : IEquatable<CountId>
{
    private CountId(string value)
    {
        this.IdValue = value;
    }

    public static CountId Create(string value)
    {
        return new CountId(value);
    }
    protected CountId() { }
    
    public string IdValue { get; init; }

    public bool Equals(CountId? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return IdValue == other.IdValue;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((CountId)obj);
    }

    public override int GetHashCode()
    {
        return IdValue.GetHashCode();
    }
}