namespace Users.Users.Domain.ValueObject;

public class UserState
{
    public bool Active { get; set; }

    private UserState(bool active)
    {
        this.Active = active;
    }
    
    public static UserState Create(bool active)
    {
        return new UserState(active);
    }

    public override bool Equals(object obj)
    {
        return obj is UserState state && Active == state.Active;
    }
}