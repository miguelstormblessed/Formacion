using Cojali.Shared.Domain.Bus.Command;

namespace Users.Shared.Users.Domain.Commands;

public class UserUpdaterCommand : Command, IEquatable<UserUpdaterCommand>
{
    private UserUpdaterCommand(string id, string name, string email, bool state, string vehicle)
    {
        Id = id;
        Name = name;
        Email = email;
        State = state;
        Vehicle = vehicle;
    }

    public string Id { get;  set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public bool State { get; set; }
    public string Vehicle { get; set; }

    public static UserUpdaterCommand Create(string id, string name, string email, bool state, string vehicle)
    {
        return new UserUpdaterCommand(id,name,email,state,vehicle);
    }

    public bool Equals(UserUpdaterCommand? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id == other.Id && Name == other.Name && Email == other.Email && State == other.State && Vehicle == other.Vehicle;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((UserUpdaterCommand)obj);
    }
    
}