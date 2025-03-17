using System.Text.Json.Serialization;
using UsersManagement.Vehicles.Domain.ValueObject;

namespace UsersManagement.Shared.Users.Domain.Requests;

public class UserRequest
{
    public string Id { get;  set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string  VehicleId { get; set; }
    
    public UserRequest(string id, string name, string email, string vehicleId)
    {
        Id = id;
        Name = name;
        Email = email;
        VehicleId = vehicleId;
    }
    
   
}