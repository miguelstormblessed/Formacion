namespace Users.Shared.Users.Domain.Responses;

public class UserResponseCtrl
{
    
    public string Name { get; set; }
    public string Email { get; set; }
    
    public string RegistrationVehicleNumber { get; set; }
    
    public string VehicleColor { get; set; }

    private UserResponseCtrl(string name, string email, string registrationVehicleNumber, string vehicleColor)
    {
        this.Name = name;
        this.Email = email;
        this.RegistrationVehicleNumber = registrationVehicleNumber;
        this.VehicleColor = vehicleColor;
        
    }

    public static UserResponseCtrl Create(string name, string email, string registrationVehicleNumber, string vehicleColor)
    {
        return new UserResponseCtrl(name, email, registrationVehicleNumber, vehicleColor);
    }
}