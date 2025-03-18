using Cojali.Shared.Domain.Bus.Event;

namespace Users.Shared.Users.Domain.DomainEvents;

public class ActivatedUserCreated : DomainEvent
{
    private ActivatedUserCreated(string identifier, string name, string email, bool state) 
        : base(identifier, null, DateTime.Now)
    {
        Name = name;
        Email = email;
        State = state;
    }
    public string Name { get; set; }
    public string Email { get; set; }
    public bool State { get; set; }

    public static ActivatedUserCreated Create(string identifier, string name, string email, bool state)
    {
        return new ActivatedUserCreated(identifier, name, email, state);
    }

    public override string EventName => "ActivatedUserCreated";
    
    
    
    public override Dictionary<string, string> ToPrimitives()
    {
        throw new NotImplementedException();
    }

    public override DomainEvent FromPrimitives(string aggregateId, Dictionary<string, string> body, string eventId, DateTime occurredOn)
    {
        throw new NotImplementedException();
    }
}