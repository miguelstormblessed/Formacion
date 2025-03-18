namespace Users.Shared.Vehicles.Domain.Exceptions;

public class VehicleNotFoundException : Exception
{
    public VehicleNotFoundException() : base("Vehicle not found") { }
}