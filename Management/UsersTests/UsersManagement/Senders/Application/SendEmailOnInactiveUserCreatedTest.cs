using Cojali.Shared.Domain.Bus.Event;
using UsersManagement.Senders.Application;
using UsersManagement.Senders.Domain;
using UsersManagement.Senders.Domain.ValueObject;
using UsersManagement.Shared.Users.Domain.DomainEvents;
using UsersManagement.Users.Domain.ValueObject;
using UsersTests.UsersManagement.Users.Domain.ValueObject;

namespace UsersTests.UsersManagement.Senders.Application;

public class SendEmailOnInactiveUserCreatedTest : SendersModuleAplicationUnitTestCase
{
    private readonly Sender _sender;
    public SendEmailOnInactiveUserCreatedTest()
    {
        this._sender = new Sender(this.SendRepository.Object);
    }

    [Fact]
    public async Task ShouldSendEmail_WhenInactiveUserCreatedEventIsCaptured()
    {
        // GIVEN
        UserId Id = UserIdMother.CreateRandom();
        UserEmail Email = UserEmailMother.CreateRandom();
        UserName UserName = UserNameMother.CreateRandom();
        
        InactiveUserCreated userEvent = InactiveUserCreated.Create(
            Id.Id,UserName.Name,Email.Email,true);
        
        SendEmailOnInactiveUserCreated emailSender = new SendEmailOnInactiveUserCreated(this._sender);
        Send send = Send.Create(
            SendTo.Create(Email.Email),
            SendSubject.Create("User Inactive Created"),
            SendMessage.Create("An inactive user has been created"));
        
        // WHEN 
        await emailSender.OnAsync((DomainEvent) userEvent);
        
        // THEN
        this.ShouldHaveCalledSendWithCorrectParametersOnce(send);

    }
}