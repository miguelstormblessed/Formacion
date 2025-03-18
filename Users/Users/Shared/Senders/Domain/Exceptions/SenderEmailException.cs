namespace Users.Shared.Senders.Domain.Exceptions;

public class SenderEmailException : Exception
{
    public SenderEmailException() : base("Sending email failed"){}
}