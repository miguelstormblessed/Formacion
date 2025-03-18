
using Microsoft.AspNetCore.Mvc;
using Users.Shared.Users.Domain.Exceptions;
using UsersManagement.Users.Application.Delete;
using UsersManagement.Users.Domain.ValueObject;

namespace UsersAPI.Controllers.Users.Delete;
[ApiController]
[ApiExplorerSettings(GroupName = "Users")]
[Route("[controller]")]
public class UserDeleterController : Controller
{
   private readonly UserDeleter _userDeleter;

   public UserDeleterController(UserDeleter userDeleter)
   {
      _userDeleter = userDeleter;
   }
   [HttpDelete]
   public IActionResult DeleteUser(string id)
   {
      try
      {
         UserId userId = UserId.Create(id);
         this._userDeleter.Execute(userId);
         return Ok();
      }
      catch (InvalidIdException ex)
      {
         return BadRequest(ex.Message);
      }
      catch (UserNotFoundException ex)
      {
         return NotFound(ex.Message);
      }
      catch (UserAlreadyDeletedException e)
      {
         return BadRequest(e.Message);
      }
      catch (Exception ex)
      {
         return BadRequest(ex.Message);
      }
      
      
   }
}