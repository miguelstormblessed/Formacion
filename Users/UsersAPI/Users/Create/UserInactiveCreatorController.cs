
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Users.Shared.Users.Domain.Requests;
using Users.Shared.Vehicles.Domain.Exceptions;
using Users.Shared.Vehicles.Domain.Responses;
using Users.Users.Application.Create;
using Users.Users.Domain;
using Users.Users.Domain.ValueObject;

namespace UsersAPI.Controllers.Users.Create;
[ApiController]
[ApiExplorerSettings(GroupName = "Users")]
[Route("[controller]")]
public class UserInactiveCreatorController : Controller
{
    
    private readonly UserInactiveCreator _userInactiveCreator;
    private readonly IHttpClientService _httpClientService;

    public UserInactiveCreatorController(UserInactiveCreator userInactiveCreator, IHttpClientService httpClientService)
    {
        _userInactiveCreator = userInactiveCreator;
        _httpClientService = httpClientService;
    }

    [HttpPost]
    public async Task<IActionResult> AddUsers(UserRequest request)
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
            
            await this._userInactiveCreator.Execute(userId, userName, userEmail, vehicleResponse);
            return CreatedAtAction(nameof(AddUsers), new { id = userId }, vehicleResponse);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}