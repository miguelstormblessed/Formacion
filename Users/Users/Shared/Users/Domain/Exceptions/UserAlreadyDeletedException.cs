namespace Users.Shared.Users.Domain.Exceptions;

public class UserAlreadyDeletedException : Exception
{
    public UserAlreadyDeletedException() : base("User is already deleted"){}
}