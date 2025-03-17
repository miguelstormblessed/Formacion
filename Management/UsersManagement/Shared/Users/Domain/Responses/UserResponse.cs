namespace UsersManagement.Shared.Users.Domain.Responses;

public class UserResponse : IEquatable<UserResponse>
{
    private UserResponse(string id, string name, string email, bool state)
    {
        Id = id;
        Name = name;
        Email = email;
        State = state;
    }
    
    public string Id { get;  set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public bool State { get; set; }

    public static UserResponse Create(string id, string name, string email, bool state)
    {
        return new UserResponse(id, name, email, state);
    }
    
    protected UserResponse(){}

    public bool Equals(UserResponse? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id == other.Id && Name == other.Name && Email == other.Email && State == other.State;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((UserResponse)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name, Email, State);
    }
}