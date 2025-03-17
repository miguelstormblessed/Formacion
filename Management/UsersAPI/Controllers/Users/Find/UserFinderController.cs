using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using UsersManagement.Shared.Users.Domain.Exceptions;
using UsersManagement.Shared.Users.Domain.Responses;
using UsersManagement.Users.Application.Find;
using UsersManagement.Users.Domain;
using UsersManagement.Users.Domain.ValueObject;

namespace UsersAPI.Controllers.Users.Find;

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

            UserResponseCtrl userResponseCtrl = UserResponseCtrl.Create(result.Name.Name, result.Email.Email, result.Vehicle.VehicleRegistration, result.Vehicle.VehicleColor);

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