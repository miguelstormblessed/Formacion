using System.Net;
using Microsoft.AspNetCore.Mvc;
using Users.Shared.Users.Domain.Exceptions;
using Users.Shared.Users.Domain.Requests;
using Users.Shared.Vehicles.Domain.Exceptions;
using Users.Shared.Vehicles.Domain.Responses;
using Users.Users.Application.Update;
using Users.Users.Domain;
using Users.Users.Domain.ValueObject;

namespace UsersAPI.Controllers.Users.Update
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "Users")]
    [Route("[controller]")]
    public class UserUpdaterController : Controller
    {
        private readonly UserUpdater _userUpdater;
        private readonly IHttpClientService _httpClientService;
        public UserUpdaterController(UserUpdater userUpdater, IHttpClientService httpClientService)
        {
            _userUpdater = userUpdater;
            _httpClientService = httpClientService;
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
                
                HttpResponseMessage vehicleHttpResponseMessage = await _httpClientService.GetAsync($"https://localhost:7239/VehicleFinder?id={request.VehicleId}");
                if (vehicleHttpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new VehicleNotFoundException();
                }

                string content = vehicleHttpResponseMessage.Content.ReadAsStringAsync().Result;
                VehicleResponse? vehicleResponse = VehicleResponse.FromJson(content);
                
                await _userUpdater.Execute(userId, userName, userEmail, userState, vehicleResponse);
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
