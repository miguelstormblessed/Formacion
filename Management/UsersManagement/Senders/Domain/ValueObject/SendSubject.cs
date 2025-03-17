namespace UsersManagement.Senders.Domain.ValueObject;

public class SendSubject
{
    private SendSubject(string subject)
    {
        this.Subject = subject;
    }

    public static SendSubject Create(string subject)
    {
        return new SendSubject(subject);
    }
    public string Subject { get; set; }

    public override bool Equals(object obj)
    {
        return obj is SendSubject subject && this.Subject == subject.Subject;
    }
}