using Microsoft.AspNetCore.Mvc;
using UsersManagement.Counters.Application.Finder;
using UsersManagement.Counters.Domain;
using UsersManagement.Shared.Counters.Domain.Responses;

namespace UsersAPI.Controllers.Counters;
[ApiController]
[ApiExplorerSettings(GroupName = "Counters")]
[Route("[controller]")]
public class UsersCountController : Controller
{
   private readonly UsersCounterFinder _userFinder;

   public UsersCountController(UsersCounterFinder userFinder)
   {
      _userFinder = userFinder;
   }

   [HttpGet]
   public async Task<IActionResult> GetUserCount()
   {
     Count count = await this._userFinder.Execute();
     CountResponse response = new CountResponse(count.ActiveUsers, count.InactiveUsers);
     return Ok(response); 
   }
}