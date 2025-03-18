using Cojali.Shared.Domain.Bus.Event;

namespace Users.Shared.Bookings.Domain.DomainEvents;

public class BookingDeleted : DomainEvent
{

    private BookingDeleted(string identifier, string date, string vehicleId, string userId,
        string name, string email, string vehicleRegistrationNumber)
        : base(identifier, null, DateTime.Now)
    {
        Date = date;
        VehicleId = vehicleId;
        UserId = userId;
        Name = name;
        Email = email;
        VehicleRegistrationNumber = vehicleRegistrationNumber;
    }
    
    public string Date { get; set; }
    
    public string VehicleId { get; set; }
    
    public string UserId { get ;set; }
    
    public string Name { get; set; }
    
    public string VehicleRegistrationNumber { get; set; }
    
    public string Email { get; set; }

    public static BookingDeleted Create(string identifier, string date, string vehicleId, string userId, string name, string email, string vehicleRegistrationNumber)
    {
        return new BookingDeleted(identifier, date, vehicleId, userId, name, email, vehicleRegistrationNumber);
    }
    
    public override Dictionary<string, string> ToPrimitives()
    {
        throw new NotImplementedException();
    }

    public override DomainEvent FromPrimitives(string aggregateId, Dictionary<string, string> body, string eventId, DateTime occurredOn)
    {
        throw new NotImplementedException();
    }

    public override string EventName => "BookingDeleted";
}