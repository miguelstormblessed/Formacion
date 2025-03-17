using Cojali.Shared.Domain.Bus.Event;
using UsersManagement.Senders.Application;
using UsersManagement.Senders.Domain;
using UsersManagement.Senders.Domain.ValueObject;
using UsersManagement.Shared.Users.Domain.DomainEvents;
using UsersManagement.Users.Domain.ValueObject;
using UsersTests.UsersManagement.Users.Domain.ValueObject;

namespace UsersTests.UsersManagement.Senders.Application;

public class SendEmailOnUserUpdatedTest : SendersModuleAplicationUnitTestCase
{
    private readonly Sender _sender;

    public SendEmailOnUserUpdatedTest()
    {
        this._sender = new Sender(this.SendRepository.Object);
    }
     [Fact]
    public async Task ShouldSendEmail_WhenUserUpdatedEventIsCaptured()
    {
        // GIVEN
        UserId Id = UserIdMother.CreateRandom();
        UserEmail Email = UserEmailMother.CreateRandom();
        UserName UserName = UserNameMother.CreateRandom();
        UserEmail oldEmail = UserEmailMother.CreateRandom();
        UserName oldUserName = UserNameMother.CreateRandom();

        UserUpdated userUpdatedEvent = UserUpdated.Create(
            Id.Id, UserName.Name, Email.Email, true, oldUserName.Name, oldEmail.Email, false);
        SendEmailOnUserUpdated sendEmailOnUserDeleted = new SendEmailOnUserUpdated(this._sender);
        Send send = Send.Create(SendTo.Create(Email.Email),
            SendSubject.Create("User Updated"),
            SendMessage.Create("User has been updated"));
        // WHEN
        await sendEmailOnUserDeleted.OnAsync((DomainEvent)userUpdatedEvent);
        // THEN
        this.ShouldHaveCalledSendWithCorrectParametersOnce(send);
    }
}