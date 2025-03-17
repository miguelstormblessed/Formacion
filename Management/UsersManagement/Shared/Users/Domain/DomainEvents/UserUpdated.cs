using Cojali.Shared.Domain.Bus.Event;

namespace UsersManagement.Shared.Users.Domain.DomainEvents;

public class UserUpdated : DomainEvent, IEquatable<UserUpdated>
{
    private UserUpdated(string identifier, string name, string email, bool state, string oldName, string oldEmail, bool oldState ) 
        : base(identifier, null, DateTime.Today)
    {
        this.Name = name;
        this.Email = email;
        this.State = state;
        this.OldName = oldName;
        this.OldState = oldState;
        this.OldEmail = oldEmail;
    }

    public string Name { get; set; }
    public string Email { get; set; }
    public bool State { get; set; }
    
    public string OldName { get; set; }
    
    public string OldEmail { get; set; }
    
    public bool OldState { get; set; }

    public static UserUpdated Create(
        string identifier, string name, string email, bool state, 
        string oldName, string oldEmail, bool oldState)
    {
        return new UserUpdated(identifier, name, email, state, oldName, oldEmail, oldState);
    }
    public override Dictionary<string, string> ToPrimitives()
    {
        throw new NotImplementedException();
    }

    public override DomainEvent FromPrimitives(string aggregateId, Dictionary<string, string> body, string eventId, DateTime occurredOn)
    {
        throw new NotImplementedException();
    }

    public override string EventName => "UserUpdated";

    public bool Equals(UserUpdated? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return base.Equals(other) && Name == other.Name && Email == other.Email && State == other.State;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((UserUpdated)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), Name, Email, State);
    }
}