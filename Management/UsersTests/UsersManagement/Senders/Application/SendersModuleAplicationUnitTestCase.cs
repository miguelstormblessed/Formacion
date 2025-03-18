/*using Cojali.Shared.Domain.Bus.Event;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using UsersManagement.Senders.Application;
using UsersManagement.Senders.Domain;
using UsersManagement.Users.Domain;
using UsersManagement.Users.Infrastructure;

namespace UsersTests.UsersManagement.Senders.Application;

public class SendersModuleAplicationUnitTestCase
{
    
    public SendersModuleAplicationUnitTestCase()
    {
        this.UserRepository = new Mock<IUserRepository>();
        this.SendRepository = new Mock<ISendRepository>();
        this.EventBus = new Mock<IEventBus>();
    }
    protected Mock<IUserRepository> UserRepository { get; }
    protected Mock<ISendRepository> SendRepository { get; }
    protected Mock<IEventBus> EventBus { get; }

    protected void ShouldHaveCalledSendWithCorrectParametersOnce(Send send)
    {
        this.SendRepository.Verify(x => x.Send(It.Is<Send>(s => s.Equals(send))), Times.Once);

    }
    protected void ShouldSaveUsers(Usuario user)
    {
        this.UserRepository.Setup(x => x.Save(user));
    }
    protected void ShouldHaveCalledSaveWithCorrectParametersOnce(Usuario user)
    {
        this.UserRepository.Verify(x => x.Save(user), Times.Once);
    }

    protected void ShouldSendEmail(Send send)
    {
        this.SendRepository.Setup(x => x.Send(send));
    }
}*/