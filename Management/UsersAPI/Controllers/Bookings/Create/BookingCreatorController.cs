using System.Net;
using Microsoft.AspNetCore.Mvc;
using UsersManagement.Bookings.Application.Create;
using UsersManagement.Bookings.Domain.ValueObject;
using UsersManagement.Shared.Bookings.Domain.Exceptions;
using UsersManagement.Shared.Bookings.Domain.Requests;
using UsersManagement.Shared.HttpClient;
using UsersManagement.Shared.Users.Domain.Exceptions;
using UsersManagement.Shared.Users.Domain.Responses;
using UsersManagement.Shared.Vehicles.Domain.Exceptions;

namespace UsersAPI.Controllers.Bookings.Create;
[ApiController]
[ApiExplorerSettings(GroupName = "Bookings")]
[Route("[controller]")]
public class BookingCreatorController : Controller
{
    private readonly BookingCreator _bookingCreator;
    private readonly IHttpClientService _httpClientService;

    public BookingCreatorController(BookingCreator bookingCreator, IHttpClientService httpClientService)
    {
        _bookingCreator = bookingCreator;
        _httpClientService = httpClientService;
    }

    [HttpPost]
    public async Task<IActionResult> CeateBooking(BookingRequestCtrl requestCtrl)
    {
        try
        {
            BookingId id = BookingId.Create(requestCtrl.Id);
            BookingDate date = BookingDate.Create(requestCtrl.Date);
            HttpResponseMessage userHttpResponseMessage = await _httpClientService.GetAsync($"https://localhost:7172/UserFinder?id={requestCtrl.UserId}");
            if (userHttpResponseMessage.StatusCode == HttpStatusCode.NotFound)
            {
                throw new UserNotFoundException();
            }
            string content = userHttpResponseMessage.Content.ReadAsStringAsync().Result;
            UserResponse userResponse = UserResponse.FromJson(content);
            
            await this._bookingCreator.Execute(id, date, requestCtrl.VehicleId, userResponse);
            return CreatedAtAction(nameof(CeateBooking), new { id = id }, null);
        }
        catch (InvalidIdException e)
        {
            return BadRequest(e.Message);
        }
        catch (InvalidDateFormatException e)
        {
            return BadRequest(e.Message);
        }
        catch (VehicleNotFoundException e)
        {
            return BadRequest(e.Message);
        }
        catch (UserNotFoundException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
        
    }
}