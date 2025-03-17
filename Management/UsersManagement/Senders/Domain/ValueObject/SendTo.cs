namespace UsersManagement.Senders.Domain.ValueObject;

public class SendTo
{
    private SendTo(string to)
    {
        this.To = to;
    }

    public static SendTo Create(string to)
    {
        return new SendTo(to);
    }
    public string To { get; set; }

    public override bool Equals(object obj)
    {
        return obj is SendTo sendTo && this.To == sendTo.To;
    }
}