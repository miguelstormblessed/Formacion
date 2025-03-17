namespace UsersManagement.Shared.Users.Domain.Exceptions;

public class InvalidEmailException : Exception
{
    public InvalidEmailException() : base("Invalid email address.") { }
}