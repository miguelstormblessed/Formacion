using Microsoft.AspNetCore.Mvc;
using UsersManagement.Bookings.Application.Create;
using UsersManagement.Bookings.Domain.ValueObject;
using UsersManagement.Shared.Bookings.Domain.Exceptions;
using UsersManagement.Shared.Bookings.Domain.Requests;
using UsersManagement.Shared.Users.Domain.Exceptions;
using UsersManagement.Shared.Vehicles.Domain.Exceptions;

namespace UsersAPI.Controllers.Bookings.Create;
[ApiController]
[ApiExplorerSettings(GroupName = "Bookings")]
[Route("[controller]")]
public class BookingCreatorController : Controller
{
    private readonly BookingCreator _bookingCreator;

    public BookingCreatorController(BookingCreator bookingCreator)
    {
        _bookingCreator = bookingCreator;
    }

    [HttpPost]
    public async Task<IActionResult> CeateBooking(BookingRequestCtrl requestCtrl)
    {
        try
        {
            BookingId id = BookingId.Create(requestCtrl.Id);
            BookingDate date = BookingDate.Create(requestCtrl.Date);
            await this._bookingCreator.Execute(id, date, requestCtrl.VehicleId, requestCtrl.UserId);
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