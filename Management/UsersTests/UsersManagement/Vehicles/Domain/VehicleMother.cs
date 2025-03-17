using UsersManagement.Vehicles.Domain;
using UsersTests.UsersManagement.Vehicles.Domain.ValueObject;

namespace UsersTests.UsersManagement.Vehicles.Domain;

public class VehicleMother
{
    public static Vehicle CreateRandom()
    {
        return Vehicle.Create(
            VehicleIdMother.CreateRandom(),
            VehicleRegistrationMother.CreateRandom(),
            VehicleColorMother.CreateRandom()
            );
    }
}