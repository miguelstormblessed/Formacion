using UsersManagement.Senders.Domain;

namespace UsersManagement.Senders.Infraestructure;

public class ConsoleSender : ISendRepository
{
    public void Send(Send send)
    {
        Console.WriteLine(send.Message.Message);
    }
}