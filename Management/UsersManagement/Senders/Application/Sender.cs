using UsersManagement.Senders.Domain;

namespace UsersManagement.Senders.Application;

public class Sender
{
    private readonly ISendRepository _sender;

    public Sender(ISendRepository sender)
    {
        this._sender = sender;
    }

    public void Execute(Send send)
    {
        this._sender.Send(send);
    }
}