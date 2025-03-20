using Microsoft.AspNetCore.Mvc;
using Users.Shared.Users.Domain.Exceptions;
using Users.Shared.Users.Domain.Requests;
using Users.Users.Application.Update;
using Users.Users.Domain.ValueObject;

namespace UsersAPI.Controllers.Users.Update
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "Users")]
    [Route("[controller]")]
    public class UserUpdaterController : Controller
    {
        private readonly UserUpdater _userUpdater;

        public UserUpdaterController(UserUpdater userUpdater)
        {
            _userUpdater = userUpdater;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(UserUpdaterRequest request)
        {
            try
            {
                UserId userId = UserId.Create(request.Id);
                UserName userName = UserName.Create(request.Name);
                UserEmail userEmail = UserEmail.Create(request.Email);
                UserState userState  = UserState.Create(request.State);
                await _userUpdater.Execute(userId, userName, userEmail, userState, request.VehicleId);
                return Ok();
            }
            catch (InvalidIdException e)
            {
                return BadRequest(e.Message);
            }
            catch (InvalidUserNameException e)
            {
                return BadRequest(e.Message);
            }
            catch (InvalidEmailException e)
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
}
