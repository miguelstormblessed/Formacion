using UsersManagement.Vehicles.Domain.ValueObject;

namespace UsersTests.UsersManagement.Vehicles.Domain.ValueObject;

public class VehicleColorMother
{

    public static VehicleColor CreateRandom()
    {
        Array colors = Enum.GetValues<VehicleColor.ColorValue>();
        Random random = new Random();
        return VehicleColor.CreateVehicleColor((VehicleColor.ColorValue)colors.GetValue(random.Next(0, colors.Length)));
    }
}