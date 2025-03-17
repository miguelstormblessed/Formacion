namespace UsersManagement.Senders.Domain.ValueObject;

public class SendMessage
{
    private SendMessage(string message)
    {
        this.Message = message;
    }

    public static SendMessage Create(string message)
    {
        return new SendMessage(message);
    }
    public string Message { get; set; }

    public override bool Equals(object obj)
    {
        return obj is SendMessage message && this.Message == message.Message;
    }
}