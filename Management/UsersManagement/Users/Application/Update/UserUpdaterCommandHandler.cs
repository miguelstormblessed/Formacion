using Cojali.Shared.Domain.Bus.Command;
using UsersManagement.Shared.Users.Domain.Commands;
using UsersManagement.Users.Application.Find;
using UsersManagement.Users.Domain.ValueObject;

namespace UsersManagement.Users.Application.Update;

public class UserUpdaterCommandHandler : ICommandHandler<UserUpdaterCommand>
{
    private readonly UserUpdater userUpdater;
    public UserUpdaterCommandHandler(UserUpdater userUpdater)
    {
        this.userUpdater = userUpdater;
    }

    public async Task HandleAsync(UserUpdaterCommand command)
    {
        UserId userId = UserId.Create(command.Id);
        UserName userName = UserName.Create(command.Name);
        UserEmail userEmail = UserEmail.Create(command.Email);
        UserState userState = UserState.Create(command.State);
        string? vehicle = command.Vehicle;
        await this.userUpdater.Execute(userId, userName, userEmail, userState, vehicle);
    }
}