/*using UsersManagement.Users.Application.Find;
using UsersManagement.Users.Application.Update;
using UsersManagement.Users.Domain;
using UsersManagement.Users.Domain.ValueObject;

namespace UsersTests.Users.Application.Update;

public class UserUpdaterCommandHandlerTest : UserModuleApplicationUnitTestCase
{
    private readonly UserUpdaterCommandHandler userUpdaterCommandHandler;
    private readonly UserUpdater userUpdater;
    private readonly UserFinder userFinder;
    public UserUpdaterCommandHandlerTest()
    {
        this.userFinder = new UserFinder(this.UserRepository.Object);
        this.userUpdater = new UserUpdater(this.UserRepository.Object, this.userFinder, this.EventBus.Object,
            this.QueryBus.Object);
        this.userUpdaterCommandHandler = new UserUpdaterCommandHandler(this.userUpdater);
    }

    [Fact]
    public async Task ShouldCallUpdate()
    {
        // GIVEN
        Booking booking = BookingMother.CreateRandom();
        UserUpdaterCommand command = UserUpdaterCommand.Create(
            booking.UserResponse.Id,
            booking.UserResponse.Name,
            booking.UserResponse.Email,
            booking.UserResponse.State,
            null);
        UserId userId = UserId.Create(command.Id);
        UserName userName = UserName.Create(command.Name);
        UserEmail userEmail = UserEmail.Create(command.Email);
        UserState userState = UserState.Create(command.State);
        Usuario user = Usuario.CreateUserActivated(
            userId,userName,userEmail,null);
        
        this.ShouldFindUser(userId,user);
        // WHEN
        await this.userUpdaterCommandHandler.HandleAsync(command);
        // THEN
        this.ShouldHaveCalledUpdateWithCorrectParametersOnce(user);
    }
}*/