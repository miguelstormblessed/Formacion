namespace UsersManagement.Shared.Bookings.Domain.Requests;

public class BookingRequestCtrl
{
    public BookingRequestCtrl(string id, string date, string vehicleId, string userId)
    {
        Id = id;
        Date = date;
        VehicleId = vehicleId;
        UserId = userId;
    }
    
    public string Id {get;set;}
    
    public string Date {get;set;}
    public string VehicleId {get; set;}
    public string UserId {get; set;}
    
}