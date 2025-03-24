using System.Net;
using Microsoft.AspNetCore.Mvc;
using UsersManagement.Bookings.Application.Search;
using UsersManagement.Bookings.Domain;
using UsersManagement.Shared.Bookings.Domain.Responses;
using UsersManagement.Shared.HttpClient;
using UsersManagement.Shared.Users.Domain.Exceptions;
using UsersManagement.Shared.Users.Domain.Responses;

namespace UsersAPI.Controllers.Bookings.Search;
[ApiController]
[ApiExplorerSettings(GroupName = "Bookings")]
[Route("[controller]")]
public class BookingSearcherByUserController: Controller
{
    private readonly BookingSearcherByUser _bookingSearcherByUser;
    private readonly IHttpClientService _httpClientService;
    public BookingSearcherByUserController(BookingSearcherByUser bookingSearcherByUser, IHttpClientService httpClientService)
    {
        _bookingSearcherByUser = bookingSearcherByUser;
        _httpClientService = httpClientService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBookingsByUser(string id)
    {
        try
        {
            HttpResponseMessage userHttpResponseMessage = await _httpClientService.GetAsync($"https://localhost:7172/UserFinder?id={id}");
            if (userHttpResponseMessage.StatusCode == HttpStatusCode.NotFound)
            {
                throw new UserNotFoundException();
            }
            string content = userHttpResponseMessage.Content.ReadAsStringAsync().Result;
            UserResponse userResponse = UserResponse.FromJson(content);
            IEnumerable<Booking> bookings = await this._bookingSearcherByUser.ExecuteAsync(userResponse.Id);
            
            List<BookingResponseCtrl> response = bookings.Select(b => BookingResponseCtrl.Create(b.Date.DateValue,
                b.VehicleResponse.VehicleRegistration, b.UserResponse.Name, b.UserResponse.Email)).ToList();
            return Ok(response);
        }
        catch (InvalidIdException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (UserNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}