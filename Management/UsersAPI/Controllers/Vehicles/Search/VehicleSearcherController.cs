using Cojali.Shared.App.Api;
using Microsoft.AspNetCore.Mvc;
using UsersManagement.Shared.Vehicles.Domain.Responses;
using UsersManagement.Vehicles.Application.Search;
using UsersManagement.Vehicles.Domain;

namespace UsersAPI.Controllers.Vehicles.Search;
[ApiController]
[ApiExplorerSettings(GroupName = "Vehicle")]
[Route("[controller]")]
public class VehicleSearcherController : Controller
{
    private readonly VehicleSearcher _vehicleSearcher;

    public VehicleSearcherController(VehicleSearcher vehicleSearcher)
    {
        _vehicleSearcher = vehicleSearcher;
    }

    [HttpGet]
    public async Task<IActionResult> SearchVehicles()
    {
       IEnumerable<Vehicle> vehicles =  await this._vehicleSearcher.Execute();
       List<VehicleResponseCtrl> vehicleResponses = 
           vehicles.Select(v => new VehicleResponseCtrl(v.VehicleColor.ToString(), v.VehicleRegistration.RegistrationValue)).ToList();
       
       return Ok(vehicleResponses);
    }
}