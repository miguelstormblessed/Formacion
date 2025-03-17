using Cojali.Shared.App.Api;
using Microsoft.AspNetCore.Mvc;
using UsersManagement.Shared.Users.Domain.Exceptions;
using UsersManagement.Shared.Vehicles.Domain.Exceptions;
using UsersManagement.Shared.Vehicles.Domain.Requests;
using UsersManagement.Vehicles.Application.Create;
using UsersManagement.Vehicles.Domain.ValueObject;

namespace UsersAPI.Controllers.Vehicles.Create;
[ApiController]
[ApiExplorerSettings(GroupName = "Vehicle")]
[Route("[controller]")]
public class VehicleCreatorController : Controller
{
    private readonly VehicleCreator _vehicleCreator;

    public VehicleCreatorController(VehicleCreator vehicleCreator)
    {
        _vehicleCreator = vehicleCreator;
    }

    [HttpPost]
    public IActionResult AddVehicles(VehicleRequest request)
    {
        try
        {
            VehicleId id = VehicleId.Create(request.Id);
            VehicleRegistration registration = VehicleRegistration.Create(request.RegistrationNumber);
            VehicleColor color = null;
            
            if (Enum.TryParse(request.VehicleColor, out VehicleColor.ColorValue colorValue))
            {
                color = VehicleColor.CreateVehicleColor(colorValue);
            }
            else
            {
                throw new InvalidColorException();
            }
            

            this._vehicleCreator.Execute(id, registration, color);
            return CreatedAtAction(nameof(AddVehicles), new { id = id }, null);
        }
        catch (InvalidIdException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidVehicleRegistrationNumberException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidColorException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        
    }
}