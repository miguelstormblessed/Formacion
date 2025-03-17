using Microsoft.AspNetCore.Mvc;
using UsersManagement.Bookings.Application.Find;
using UsersManagement.Bookings.Domain;
using UsersManagement.Bookings.Domain.ValueObject;
using UsersManagement.Shared.Bookings.Domain.Exceptions;
using UsersManagement.Shared.Bookings.Domain.Requests;
using UsersManagement.Shared.Bookings.Domain.Responses;
using UsersManagement.Shared.Users.Domain.Exceptions;

namespace UsersAPI.Controllers.Bookings.Find;
[ApiController]
[ApiExplorerSettings(GroupName = "Bookings")]
[Route("[controller]")]
public class BookingFinderController : Controller
{
    private readonly BookingFinder _bookingFinder;

    public BookingFinderController(BookingFinder bookingFinder)
    {
        _bookingFinder = bookingFinder;
    }

    [HttpGet]
    public IActionResult GetBooking(string id)
    {
        try
        {
            BookingId bookingId = BookingId.Create(id);
            Booking? booking = _bookingFinder.Execute(bookingId);
            
            BookingResponseCtrl response = BookingResponseCtrl.Create(booking.Date.DateValue,
                booking.VehicleResponse.VehicleRegistration, booking.UserResponse.Name, booking.UserResponse.Email);
            return Ok(response);
        }
        catch (InvalidIdException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (BookingNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        
    }
}