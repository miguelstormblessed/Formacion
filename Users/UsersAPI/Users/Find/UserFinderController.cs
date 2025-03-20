using Microsoft.AspNetCore.Mvc;
using Users.Shared.Users.Domain.Exceptions;
using Users.Shared.Users.Domain.Responses;
using Users.Users.Application.Find;
using Users.Users.Domain;
using Users.Users.Domain.ValueObject;

namespace UsersAPI.Users.Find;

[ApiController]
[ApiExplorerSettings(GroupName = "Users")]
[Route("[controller]")]
public class UserFinderController : ControllerBase
{
    private readonly UserFinder _userFinder;

    public UserFinderController(UserFinder userFinder)
    {
        _userFinder = userFinder;
    }
    [HttpGet]
    public IActionResult FindUser(string id)
    {
        
        try
        {
            UserId userId = UserId.Create(id);
            Usuario result = _userFinder.Execute(userId);

            UserResponse userResponseCtrl = UserResponse.Create(result.Id.Id, result.Name.Name, result.Email.Email, result.State.Active);

            return Ok(userResponseCtrl);
        }
        catch (InvalidIdException e)
        {
            return BadRequest(e.Message);
        }
        catch (UserNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
}