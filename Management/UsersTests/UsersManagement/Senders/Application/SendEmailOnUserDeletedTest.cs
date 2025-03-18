/*using Cojali.Shared.Domain.Bus.Event;
using UsersManagement.Senders.Application;
using UsersManagement.Senders.Domain;
using UsersManagement.Senders.Domain.ValueObject;
using UsersManagement.Shared.Users.Domain.DomainEvents;
using UsersManagement.Users.Domain.ValueObject;
using UsersTests.UsersManagement.Users.Domain.ValueObject;

namespace UsersTests.UsersManagement.Senders.Application;

public class SendEmailOnUserDeletedTest : SendersModuleAplicationUnitTestCase
{
    private readonly Sender _sender;

    public SendEmailOnUserDeletedTest()
    {
        this._sender = new Sender(this.SendRepository.Object);
    }

    [Fact]
    public async Task ShouldSendEmail_WhenUserDeletedEventIsCaptured()
    {
        // GIVEN
        UserId Id = UserIdMother.CreateRandom();
        UserEmail Email = UserEmailMother.CreateRandom();
        UserName UserName = UserNameMother.CreateRandom();
        
        UserDeleted userDeletedEvent = UserDeleted.Create(
            Id.Id,UserName.Name,Email.Email,true);
        
        SendEmailOnUserDeleted sendEmailOnUserDeleted = new SendEmailOnUserDeleted(_sender);
        Send send = Send.Create(
            SendTo.Create(Email.Email),
            SendSubject.Create("User Deleted"),
            SendMessage.Create("User has been deleted"));
        // WHEN
        await sendEmailOnUserDeleted.OnAsync((DomainEvent) userDeletedEvent);
        // THEN
        this.ShouldHaveCalledSendWithCorrectParametersOnce(send);
    }
}*/