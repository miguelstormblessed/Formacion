using Microsoft.AspNetCore.Mvc;
using UsersManagement.Bookings.Application.Search;
using UsersManagement.Bookings.Domain;
using UsersManagement.Shared.Bookings.Domain.Responses;
using UsersManagement.Shared.Users.Domain.Exceptions;

namespace UsersAPI.Controllers.Bookings.Search;
[ApiController]
[ApiExplorerSettings(GroupName = "Bookings")]
[Route("[controller]")]
public class BookingSearcherByUserController: Controller
{
    private readonly BookingSearcherByUser _bookingSearcherByUser;

    public BookingSearcherByUserController(BookingSearcherByUser bookingSearcherByUser)
    {
        _bookingSearcherByUser = bookingSearcherByUser;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBookingsByUser(string id)
    {
        try
        {
            IEnumerable<Booking> bookings = await this._bookingSearcherByUser.ExecuteAsync(id);
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