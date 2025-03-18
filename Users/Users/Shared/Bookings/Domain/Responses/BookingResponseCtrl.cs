namespace Users.Shared.Bookings.Domain.Responses;

public class BookingResponseCtrl
{

    private BookingResponseCtrl(string date, string vehicleRegistrationNumber, string name, string email)
    {
        Date = date;
        VehicleRegistrationNumber = vehicleRegistrationNumber;
        Name = name;
        Email = email;
    }

    public static BookingResponseCtrl Create(string date, string vehicleRegistrationNumber, string name, string email)
    {
        return new BookingResponseCtrl(date, vehicleRegistrationNumber, name, email);
    }
    public string Date { get; set; }
    
    public string VehicleRegistrationNumber { get; set; }
    
    public string Name { get; set; }
    
    public string Email { get; set; }
}