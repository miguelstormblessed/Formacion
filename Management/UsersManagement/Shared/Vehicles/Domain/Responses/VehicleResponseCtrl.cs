namespace UsersManagement.Shared.Vehicles.Domain.Responses;

public class VehicleResponseCtrl
{
    public VehicleResponseCtrl(string vehicleColor, string vehicleRegistration, string id)
    {
        VehicleColor = vehicleColor;
        VehicleRegistration = vehicleRegistration;
        Id = id;
    }

    public string VehicleColor { get; set; }
    public string VehicleRegistration { get; set; }
    
    public string Id {get; set;}
}