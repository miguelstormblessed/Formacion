namespace Users.Shared.Vehicles.Domain.Requests;

public class VehicleRequest
{
    public string Id { get; set; }
    public string RegistrationNumber { get; set; }
    public string VehicleColor { get; set; }

    public VehicleRequest(string id, string registrationNumber, string vehicleColor)
    {
        Id = id;
        RegistrationNumber = registrationNumber;
        VehicleColor = vehicleColor;
    }
}