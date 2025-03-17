namespace UsersManagement.Shared.Vehicles.Domain.Responses;

public class VehicleResponseCtrl
{
    public VehicleResponseCtrl(string color, string registrationNumber)
    {
        Color = color;
        RegistrationNumber = registrationNumber;
    }

    public string Color { get; set; }
    public string RegistrationNumber { get; set; }
}