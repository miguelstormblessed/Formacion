using Cojali.Shared.Domain.Bus.Event;
using UsersManagement.Senders.Application;
using UsersManagement.Senders.Domain;
using UsersManagement.Senders.Domain.ValueObject;
using UsersManagement.Shared.Users.Domain.DomainEvents;
using UsersManagement.Users.Domain.ValueObject;
using UsersTests.UsersManagement.Users.Domain.ValueObject;

namespace UsersTests.UsersManagement.Senders.Application;

public class SendEmailOnActivatedUserCreatedTest : SendersModuleAplicationUnitTestCase
{
    private readonly Sender _sender;
    public SendEmailOnActivatedUserCreatedTest()
    {
        this._sender = new Sender(this.SendRepository.Object);
    }
    
    [Fact]
    public async Task ShouldSendEmail_WhenActiveUserCreatedEventIsCaptured()
    {
        // GIVEN
        UserId Id = UserIdMother.CreateRandom();
        UserEmail Email = UserEmailMother.CreateRandom();
        UserName UserName = UserNameMother.CreateRandom();
        
        ActivatedUserCreated userEvent = ActivatedUserCreated.Create(
            Id.Id,UserName.Name,Email.Email,true);
        
        SendEmailOnActivatedUserCreated handler = new SendEmailOnActivatedUserCreated(this._sender);
        
        Send send = Send.Create(
            SendTo.Create(Email.Email),
            SendSubject.Create("User Created"),
            SendMessage.Create("Se ha creado un usuario"));
        
        this.ShouldSendEmail(send);
        // WHEN
        await handler.OnAsync((DomainEvent) userEvent);
        // THEN
        this.ShouldHaveCalledSendWithCorrectParametersOnce(send);

    }
}