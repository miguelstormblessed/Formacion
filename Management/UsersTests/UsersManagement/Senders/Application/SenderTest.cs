using Microsoft.AspNetCore.Mvc.Testing;
using UsersManagement.Senders.Application;
using UsersManagement.Senders.Domain;
using UsersTests.UsersManagement.Senders.Domain;

namespace UsersTests.UsersManagement.Senders.Application;

public class SenderTest : BookingsModuleApplicationTestCase
{
    private readonly Sender _sender;
    
    public SenderTest()
    {
        _sender = new Sender(this.SendRepository.Object);
    }

    [Fact]
    public void ShouldBeCalledOnceWithCorrectParameters()
    {
        // GIVEN 
        Send send = SendMother.CreateRandom();
        // WHEN 
        this._sender.Execute(send);
        // THEN
        this.ShouldHaveCalledSendWithCorrectParametersOnce(send);
    }
}