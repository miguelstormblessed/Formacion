namespace Users.Shared.Users.Domain.Exceptions;

public class InvalidUserNameException : Exception
{
    public InvalidUserNameException() : base("Invalid username") { }
}