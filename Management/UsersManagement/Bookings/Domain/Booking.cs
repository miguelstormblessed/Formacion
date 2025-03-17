using Cojali.Shared.Domain.Entity;
using UsersManagement.Bookings.Domain.ValueObject;
using UsersManagement.Shared.Bookings.Domain.DomainEvents;
using UsersManagement.Shared.Users.Domain.Responses;
using UsersManagement.Shared.Vehicles.Domain.Responses;

namespace UsersManagement.Bookings.Domain;

public class Booking : AggregateRoot, IEquatable<Booking>
{
    private Booking(BookingId id, BookingDate date, VehicleResponse vehicleResponse,
        UserResponse userResponse, BookingStatus status)
    {
        Id = id;
        Date = date;
        VehicleResponse = vehicleResponse;
        UserResponse = userResponse;
        Status = status;
    }
    protected Booking(){}
    
    public BookingId Id { get; set; }
    
    public BookingDate Date { get; set; }
    
    public VehicleResponse VehicleResponse { get; set; }
    
    public UserResponse UserResponse { get; set; }
    
    public BookingStatus Status { get; set; }

    public static Booking Create(BookingId id, BookingDate bookingDate, VehicleResponse vehicleResponse,
        UserResponse userResponse)
    {
        Booking newBooking = new Booking(id, bookingDate, vehicleResponse, userResponse, BookingStatus.Create(false));
        
        BookingCreated bookingCreatedDomainEvent = BookingCreated.Create(id.IdValue, bookingDate.DateValue, vehicleResponse.Id, userResponse.Id,
            userResponse.Email, vehicleResponse.VehicleRegistration, userResponse.Name);
        
        newBooking.Record(bookingCreatedDomainEvent);
        return newBooking;
    }
    
    public static Booking CreateBookingMother(BookingId id, BookingDate bookingDate, VehicleResponse vehicleResponse,
        UserResponse userResponse)
    {
        return new Booking(id, bookingDate, vehicleResponse, userResponse, BookingStatus.Create(false));
    }

    public void Delete()
    {
        BookingDeleted bookingDeleted = BookingDeleted.Create(
            this.Id.IdValue, 
            this.Date.DateValue, 
            this.VehicleResponse.Id, 
            this.UserResponse.Id, 
            this.UserResponse.Email, 
            this.VehicleResponse.VehicleRegistration, 
            this.UserResponse.Name);
        this.Record(bookingDeleted);
    }

    public bool Equals(Booking? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id.Equals(other.Id) && Date.Equals(other.Date) && VehicleResponse.Equals(other.VehicleResponse) && UserResponse.Equals(other.UserResponse) && Status.Equals(other.Status);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Booking)obj);
    }
    
}