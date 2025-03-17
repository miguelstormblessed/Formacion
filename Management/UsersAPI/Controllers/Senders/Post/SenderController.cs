using Microsoft.AspNetCore.Mvc;
using UsersManagement.Senders.Application;
using UsersManagement.Senders.Domain;
using UsersManagement.Senders.Domain.ValueObject;
using UsersManagement.Shared.Senders.Domain.Requests;

namespace UsersAPI.Controllers.Senders.Post;
[ApiController]
[ApiExplorerSettings(GroupName = "Senders")]
[Route("[controller]")]
public class SenderController : Controller
{
    private readonly Sender _sender;

    public SenderController(Sender sender)
    {
        this._sender = sender;
    }
    [HttpPost]
    public IActionResult Send_(SenderRequest request)
    {
        SendTo sendTo = SendTo.Create(request.SendTo);
        SendSubject sendSubject = SendSubject.Create(request.SendSubject);
        SendMessage sendMessage = SendMessage.Create(request.SendMessage);
        Send send = Send.Create(
            sendTo, sendSubject, sendMessage);
        
        this._sender.Execute(send);
        
        return Ok();
    }
}