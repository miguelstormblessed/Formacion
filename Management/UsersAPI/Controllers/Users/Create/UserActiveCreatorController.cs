using Bogus.DataSets;
using Microsoft.AspNetCore.Mvc;
using UsersManagement.Shared.Users.Domain.Requests;
using UsersManagement.Users.Application.Create;
using UsersManagement.Users.Domain;
using UsersManagement.Users.Domain.ValueObject;
using UsersManagement.Vehicles.Domain.ValueObject;

namespace UsersAPI.Controllers.Users.Create
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "Users")]
    [Route("[controller]")]
    public class UserActiveCreatorController : Controller
    {
        private readonly UserActiveCreator _userActiveCreator;
        public UserActiveCreatorController(UserActiveCreator userActiveCreator)
        {
            _userActiveCreator = userActiveCreator;
        }

        [HttpPost]
        public async Task<IActionResult> AddUsers(UserRequest? request) //UserCreatorRequest
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

                await this._userActiveCreator.Execute(userId, userName, userEmail, request.VehicleId);
                return CreatedAtAction(nameof(AddUsers), new { id = userId }, null);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }
    }
}
