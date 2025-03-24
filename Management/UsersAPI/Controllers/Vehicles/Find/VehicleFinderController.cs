using Microsoft.AspNetCore.Mvc;
using UsersManagement.Shared.Users.Domain.Exceptions;
using UsersManagement.Shared.Vehicles.Domain.Exceptions;
using UsersManagement.Shared.Vehicles.Domain.Responses;
using UsersManagement.Vehicles.Application.Find;
using UsersManagement.Vehicles.Domain;
using UsersManagement.Vehicles.Domain.ValueObject;

namespace UsersAPI.Controllers.Vehicles.Find;
[ApiController]
[ApiExplorerSettings(GroupName = "Vehicle")]
[Route("[controller]")]
public class VehicleFinderController : Controller
{
    private readonly VehicleFinder _vehicleFinder;

    public VehicleFinderController(VehicleFinder vehicleFinder)
    {
        _vehicleFinder = vehicleFinder;
    }

    [HttpGet]
    public IActionResult GetById(string id)
    {
        try
        {
            VehicleId vehicleId = VehicleId.Create(id);
            Vehicle vehicle = this._vehicleFinder.Execute(vehicleId);
            VehicleResponseCtrl responseCtrl = new VehicleResponseCtrl(vehicle.VehicleColor.ToString(),
                vehicle.VehicleRegistration.RegistrationValue, vehicle.Id.IdValue);
            return Ok(responseCtrl);
        }
        catch (VehicleNotFoundException ex)
        {
            return NotFound(ex.Message);
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