using Microsoft.AspNetCore.Mvc;
using Users.Shared.Users.Domain.Responses;
using UsersManagement.Users.Application.Search;
using UsersManagement.Users.Domain;

namespace UsersAPI.Controllers.Users.Search
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "Users")]
    [Route("[controller]")]
    public class UserSearcherController : ControllerBase
    {
        private readonly UserSearcher _userSearcher;

        public UserSearcherController(UserSearcher userSearcher)
        {
            _userSearcher = userSearcher;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            IEnumerable<Usuario> users = await _userSearcher.ExecuteAsync();
            List<UserResponseCtrl> response = 
                users.Select(u => UserResponseCtrl.Create(u.Name.Name, u.Email.Email, u.Vehicle.VehicleRegistration, u.Vehicle.VehicleColor)).ToList();
            
            return Ok(response);
        }
    }
}
