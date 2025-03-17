using Cojali.Shared.Domain.Bus.Query;
using Microsoft.AspNetCore.Mvc;
using UsersManagement.Bookings.Application.Search;
using UsersManagement.Bookings.Domain;
using UsersManagement.Shared.Bookings.Domain.Responses;
using UsersManagement.Shared.Users.Domain.Exceptions;
using UsersManagement.Shared.Vehicles.Domain.Exceptions;

namespace UsersAPI.Controllers.Bookings.Search;
[ApiController]
[ApiExplorerSettings(GroupName = "Bookings")]
[Route("[controller]")]
public class BookingSearcherByVehicleController : Controller
{
    private readonly BookingSearcherByVehicle _bookingSearcherByVehicle;
    
    public BookingSearcherByVehicleController(BookingSearcherByVehicle bookingSearcherByVehicle)
    {
        _bookingSearcherByVehicle = bookingSearcherByVehicle;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllBookingsByVehicle(string id)
    {
        try
        {
            IEnumerable<Booking> bookings = await this._bookingSearcherByVehicle.ExecuteAsync(id);
            List<BookingResponseCtrl> response = bookings.Select(b => BookingResponseCtrl.Create(b.Date.DateValue,
                b.VehicleResponse.VehicleRegistration, b.UserResponse.Name, b.UserResponse.Email)).ToList();
            return Ok(response);
        }
        catch (InvalidIdException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (VehicleNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}