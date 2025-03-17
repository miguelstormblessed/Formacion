using UsersManagement.Vehicles.Domain.ValueObject;

namespace UsersTests.UsersManagement.Vehicles.Domain.ValueObject;

public class VehicleIdMother
{
    public static VehicleId CreateRandom()
    {
        return VehicleId.Create(Guid.NewGuid().ToString());
    }
}