using Cojali.Shared.Domain.Bus.Event;

namespace UsersManagement.Shared.Users.Domain.DomainEvents;

public class UserDeleted : DomainEvent
{
    private UserDeleted(string identifier, string name, string email, bool state ) 
        : base(identifier, null, DateTime.Now)
    {
        this.Name = name;
        this.Email = email;
        this.State = state;
    }
    
    public string Name {get; set;}
    
    public string Email {get; set;}
    
    public bool State {get; set;}

    public static UserDeleted Create(string identifier, string name, string email, bool state)
    {
        return new UserDeleted(identifier, name, email, state);
    }

    public override Dictionary<string, string> ToPrimitives()
    {
        throw new NotImplementedException();
    }

    public override DomainEvent FromPrimitives(string aggregateId, Dictionary<string, string> body, string eventId, DateTime occurredOn)
    {
        throw new NotImplementedException();
    }

    public override string EventName => "UserDeleted";
}