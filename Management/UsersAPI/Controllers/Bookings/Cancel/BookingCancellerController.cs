using Microsoft.AspNetCore.Mvc;
using Users.Shared.Users.Domain.Exceptions;
using UsersManagement.Bookings.Application.Cancel;
using UsersManagement.Bookings.Domain.ValueObject;
using UsersManagement.Shared.Bookings.Domain.Exceptions;
using UsersManagement.Shared.Bookings.Domain.Requests;

namespace UsersAPI.Controllers.Bookings.Cancel;
[ApiController]
[ApiExplorerSettings(GroupName = "Bookings")]
[Route("[controller]")]
public class BookingCancellerController: Controller
{
    private readonly BookingCanceller _bookingCanceller;

    public BookingCancellerController(BookingCanceller bookingCanceller)
    {
        _bookingCanceller = bookingCanceller;
    }
    [HttpPatch]
    public async Task<IActionResult> Cancel(BookingCancellerRequest request)
    {
        try
        {
            BookingId bookingId = BookingId.Create(request.BookingId);
            BookingStatus bookingStatus = BookingStatus.Create(request.Cancel);

            this._bookingCanceller.Execute(bookingStatus, bookingId);
            return Ok();

        }
        catch (InvalidIdException e)
        {
            return BadRequest(e.Message);
        }
        catch (BookingNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}