
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Users.Shared.Users.Domain.Requests;
using Users.Shared.Users.Domain.Responses;
using Users.Shared.Vehicles.Domain.Exceptions;
using Users.Shared.Vehicles.Domain.Responses;
using Users.Users.Application.Create;
using Users.Users.Domain;
using Users.Users.Domain.ValueObject;
using Users.Users.Infrastructure;


namespace UsersAPI.Controllers.Users.Create
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "Users")]
    [Route("[controller]")]
    public class UserActiveCreatorController : Controller
    {
        private readonly UserActiveCreator _userActiveCreator;
        private readonly IHttpClientService _httpClientService;
        public UserActiveCreatorController(UserActiveCreator userActiveCreator, IHttpClientService httpClientService)
        {
            _userActiveCreator = userActiveCreator;
            _httpClientService = httpClientService;
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

                HttpResponseMessage vehicleHttpResponseMessage = await _httpClientService.GetAsync($"https://localhost:7239/VehicleFinder?id={request.VehicleId}");
                if (vehicleHttpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new VehicleNotFoundException();
                }

                string content = vehicleHttpResponseMessage.Content.ReadAsStringAsync().Result;
                VehicleResponse? vehicleResponse = VehicleResponse.FromJson(content);

                await this._userActiveCreator.Execute(userId, userName, userEmail, vehicleResponse);
                return CreatedAtAction(nameof(AddUsers), new { id = userId }, vehicleResponse);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }
    }
}
