using UsersManagement.Senders.Domain.ValueObject;
using UsersManagement.Vehicles.Infrastructure.Mappers;

namespace UsersManagement.Senders.Domain;

public class Send
{
    private Send(SendTo to, SendSubject subject, SendMessage message)
    {
        this.To = to;
        this.Subject = subject;
        this.Message = message;
    }

    public static Send Create(SendTo sendTo, SendSubject sendSubject, SendMessage sendMessage)
    {
        return new Send(sendTo, sendSubject, sendMessage);
    }
    public SendTo To { get; set; }
    public SendSubject Subject { get; set; }
    public SendMessage Message { get; set; }
    public override bool Equals(Object obj)
    {
        return obj is Send && this.To.Equals(((Send) obj).To)
            && this.Subject.Equals(((Send)obj).Subject)
            && this.Message.Equals(((Send) obj).Message);
    }
}