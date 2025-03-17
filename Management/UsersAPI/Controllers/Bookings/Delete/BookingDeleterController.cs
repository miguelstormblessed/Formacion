using Microsoft.AspNetCore.Mvc;
using UsersManagement.Bookings.Application.Delete;
using UsersManagement.Bookings.Domain.ValueObject;
using UsersManagement.Shared.Users.Domain.Exceptions;

namespace UsersAPI.Controllers.Bookings.Delete;
[ApiController]
[ApiExplorerSettings(GroupName = "Bookings")]
[Route("[controller]")]
public class BookingDeleterController : Controller
{
    private readonly BookingDeleter _bookingDeleter;

    public BookingDeleterController(BookingDeleter bookingDeleter)
    {
        _bookingDeleter = bookingDeleter;
    }

    [HttpDelete]
    public IActionResult DeleteBooking(string id)
    {
        try
        {
            BookingId bookingId = BookingId.Create(id);
            _bookingDeleter.Execute(bookingId);
            return Ok();
        }
        catch (InvalidIdException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        
    }
}