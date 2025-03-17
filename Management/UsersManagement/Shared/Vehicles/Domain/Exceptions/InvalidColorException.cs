namespace UsersManagement.Shared.Vehicles.Domain.Exceptions;

public class InvalidColorException : Exception
{
    public InvalidColorException() : base("Invalid color")
    {
    }
}