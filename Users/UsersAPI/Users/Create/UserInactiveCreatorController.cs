
using Microsoft.AspNetCore.Mvc;
using Users.Shared.Users.Domain.Requests;
using Users.Users.Application.Create;
using Users.Users.Domain.ValueObject;

namespace UsersAPI.Controllers.Users.Create;
[ApiController]
[ApiExplorerSettings(GroupName = "Users")]
[Route("[controller]")]
public class UserInactiveCreatorController : Controller
{
    
    private readonly UserInactiveCreator _userInactiveCreator;

    public UserInactiveCreatorController(UserInactiveCreator userInactiveCreator)
    {
        _userInactiveCreator = userInactiveCreator;
    }

    [HttpPost]
    public IActionResult AddUsers(UserRequest request)
    {
        if (request == null)
        {
            return BadRequest();
        }
        try
        {
            UserId userId = UserId.Create(request.Id);
            UserName userName = UserName.Create(request.Name);
            UserEmail userEmail = UserEmail.Create(request.Email);
            this._userInactiveCreator.Execute(userId, userName, userEmail, request.VehicleId);
            return CreatedAtAction(nameof(AddUsers), new { id = userId }, null);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}