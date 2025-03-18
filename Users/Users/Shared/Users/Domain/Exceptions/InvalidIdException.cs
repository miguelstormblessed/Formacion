namespace Users.Shared.Users.Domain.Exceptions;

public class InvalidIdException : Exception
{
    public InvalidIdException() : base("Invalid Id") { }
}