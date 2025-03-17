using Microsoft.AspNetCore.Mvc;
using UsersManagement.Bookings.Application.Search;
using UsersManagement.Bookings.Domain;
using UsersManagement.Shared.Bookings.Domain.Responses;

namespace UsersAPI.Controllers.Bookings.Search;
[ApiController]
[ApiExplorerSettings(GroupName = "Bookings")]
[Route("[controller]")]
public class BookingSearcherController : Controller
{
    private readonly BookingSearcher _bookingSearcher;

    public BookingSearcherController(BookingSearcher bookingSearcher)
    {
        _bookingSearcher = bookingSearcher;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBookings()
    {
        try
        {
            IEnumerable<Booking> bookings = await this._bookingSearcher.ExecuteAsync();
            List<BookingResponseCtrl> response = bookings.Select(b => BookingResponseCtrl.Create(b.Date.DateValue, b.VehicleResponse.VehicleRegistration, b.UserResponse.Name, b.UserResponse.Email)).ToList();
            return Ok(response);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}