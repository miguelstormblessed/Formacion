using Cojali.Shared.Domain.Bus.Query;
using Users.Shared.Users.Domain.Querys;
using Users.Shared.Users.Domain.Responses;
using UsersManagement.Users.Domain;
using UsersManagement.Users.Domain.ValueObject;

namespace UsersManagement.Users.Application.Find;

public class UserFinderQueryHandler : IQueryHandler<UserFinderQuery, UserResponse>
{
    public UserFinderQueryHandler(UserFinder userFinder)
    {
        _userFinder = userFinder;
    }

    private readonly UserFinder _userFinder;

    public async Task<UserResponse> HandleAsync(UserFinderQuery query)
    {
        UserId id = UserId.Create(query.UserId);
        Usuario user = this._userFinder.Execute(id);
        return UserResponse.Create(id.Id, user.Name.Name, user.Email.Email, user.State.Active);
    }
}