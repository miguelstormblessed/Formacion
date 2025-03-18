namespace Users.Shared.Users.Domain.Requests;

public class UserUpdaterRequest
{
    public UserUpdaterRequest(string id, string name, string email, bool state,string vehicleId)
    {
        Id = id;
        Name = name;
        Email = email;
        State = state;
        VehicleId = vehicleId;
    }

    public string Id { get;  set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public bool State { get; set; }
    public string  VehicleId { get; set; }
    
    
}