namespace Users.Shared.Vehicles.Domain.Responses;

public class VehicleResponseCtrl
{
    public VehicleResponseCtrl(string vehicleColor, string vehicleRegistration)
    {
        VehicleColor = vehicleColor;
        VehicleRegistration = vehicleRegistration;
    }

    public string VehicleColor { get; set; }
    public string VehicleRegistration { get; set; }
    
}