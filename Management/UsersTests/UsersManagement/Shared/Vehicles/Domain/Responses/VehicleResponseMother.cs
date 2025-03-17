using UsersManagement.Shared.Vehicles.Domain.Responses;
using UsersTests.UsersManagement.Vehicles.Domain.ValueObject;

namespace UsersTests.UsersManagement.Shared.Vehicles.Domain.Responses;

public class VehicleResponseMother
{
    public static VehicleResponse CreateRandom()
    {
        return VehicleResponse.Create(
            VehicleIdMother.CreateRandom().IdValue,
            VehicleRegistrationMother.CreateRandom().RegistrationValue,
            VehicleColorMother.CreateRandom().Value.ToString()
        );
    }
}