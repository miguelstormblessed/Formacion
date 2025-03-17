namespace UsersManagement.Shared.Vehicles.Domain.Exceptions;

public class InvalidVehicleRegistrationNumberException : Exception
{
    public InvalidVehicleRegistrationNumberException() : base("Invalid vehicle registration number") { }
}