namespace Users.Shared.Senders.Domain.Requests;

public class SenderRequest
{
    public SenderRequest(string sendTo, string sendSubject, string sendMessage)
    {
        this.SendTo = sendTo;
        this.SendSubject = sendSubject;
        this.SendMessage = sendMessage;
    }
    
    
    public string SendTo { get; set; }
    public string SendSubject { get; set; }
    public string SendMessage { get; set; }
}