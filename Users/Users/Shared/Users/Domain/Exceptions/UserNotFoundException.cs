namespace Users.Shared.Users.Domain.Exceptions;

public class UserNotFoundException : Exception
{
    public UserNotFoundException() : base("User not found"){ }
}