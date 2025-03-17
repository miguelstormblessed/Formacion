using Moq;
using UsersManagement.Senders.Domain;

namespace UsersTests.UsersManagement.Senders.Application;

public class BookingsModuleApplicationTestCase
{
    protected BookingsModuleApplicationTestCase()
    {
        SendRepository = new Mock<ISendRepository>();
    }

    protected Mock<ISendRepository> SendRepository;
    protected void ShouldHaveCalledSendWithCorrectParametersOnce(Send send)
    {
        SendRepository.Verify(repo => repo.Send(send), Times.Once);
    }
}