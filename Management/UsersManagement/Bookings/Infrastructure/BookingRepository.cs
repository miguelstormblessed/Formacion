using System.Data;
using Cojali.Shared.Domain.Specification;
using Dapper;
using MySql.Data.MySqlClient;
using UsersManagement.Bookings.Domain;
using UsersManagement.Bookings.Domain.Specification;
using UsersManagement.Bookings.Domain.ValueObject;
using UsersManagement.Shared.Users.Domain.Responses;
using UsersManagement.Shared.Vehicles.Domain.Responses;

namespace UsersManagement.Bookings.Infrastructure;

public class BookingRepository : IBookingRepository
{
    private readonly IDbConnection _connection;

    public BookingRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<IEnumerable<Booking>> SearchAll()
    {
        using (var connection = new MySqlConnection(_connection.ConnectionString))
        {
            connection.Open();
            string query = @"
                SELECT 
                    id AS Id, 
                    bookingdate AS Date, 
                    status AS Status
                FROM 
                    booking";
            IEnumerable<Booking> bookings =  await connection.QueryAsync<Booking>(query);
            foreach (var booking in bookings)
            {
                booking.UserResponse =
                    await connection.QueryFirstAsync<UserResponse>(
                        "SELECT id as Id, email as Email, name as Name, state as State FROM user WHERE id = (SELECT user FROM booking WHERE id = @Id)",
                        new { Id = booking.Id.IdValue });
                booking.VehicleResponse = await connection.QueryFirstAsync<VehicleResponse>("SELECT id as Id, registration as VehicleRegistration, color as VehicleColor FROM vehicle WHERE id = (SELECT vehicle FROM booking WHERE id = @Id)"
                    , new { Id = booking.Id.IdValue });
            }
        
            return bookings;
        }
        
    }

    public async Task<IEnumerable<Booking>> Search(SpecificationBase<Booking> genericSpecification)
    {
        IEnumerable<Booking> bookings = null;
        using (var connection = new MySqlConnection(_connection.ConnectionString))
        {
            connection.Open();
            if (genericSpecification is BookingByUserIdSpecification)
            {
                BookingByUserIdSpecification specification = (BookingByUserIdSpecification)genericSpecification;
                string id = specification._id;
                
                string query =" SELECT id AS Id, bookingdate AS Date, status AS Status FROM booking WHERE user = @Id";
                bookings =  await connection.QueryAsync<Booking>(query, new {Id = id});
        
                foreach (var booking in bookings)
                {
                    booking.UserResponse =
                        await connection.QueryFirstAsync<UserResponse>(
                            "SELECT id as Id, email as Email, name as Name, state as State FROM user WHERE id = (SELECT user FROM booking WHERE id = @Id)",
                            new { Id = booking.Id.IdValue });
                    booking.VehicleResponse = await connection.QueryFirstAsync<VehicleResponse>("SELECT id as Id, registration as VehicleRegistration, color as VehicleColor FROM vehicle WHERE id = (SELECT vehicle FROM booking WHERE id = @Id)"
                        , new { Id = booking.Id.IdValue });
                }
                
            }else if (genericSpecification is BookingByVehicleIdSpecification)
            {
                BookingByVehicleIdSpecification specification = (BookingByVehicleIdSpecification)genericSpecification;
                string id = specification._id;
                
                string query =" SELECT id AS Id, bookingdate AS Date, status AS Status FROM booking WHERE vehicle = @Id";
                bookings =  await connection.QueryAsync<Booking>(query, new {Id = id});
                foreach (var booking in bookings)
                {
                    booking.UserResponse =
                        await connection.QueryFirstAsync<UserResponse>(
                            "SELECT id as Id, email as Email, name as Name, state as State FROM user WHERE id = (SELECT user FROM booking WHERE id = @Id)",
                            new { Id = booking.Id.IdValue });
                    booking.VehicleResponse = await connection.QueryFirstAsync<VehicleResponse>("SELECT id as Id, registration as VehicleRegistration, color as VehicleColor FROM vehicle WHERE id = (SELECT vehicle FROM booking WHERE id = @Id)"
                        , new { Id = booking.Id.IdValue });
                }
                
            }
        }
        return bookings;
    }
    

    public Booking GetBookingById(BookingId id)
    {
        using (var connection = new MySqlConnection(_connection.ConnectionString))
        {
            connection.Open();
            string query = "SELECT id as Id, bookingdate as Date ,status as Status  FROM booking WHERE id = @Id";
        
            Booking booking = connection.QuerySingleOrDefault<Booking>(query, new { Id = id.IdValue });
            if (booking == null)
            {
                return null;
            }
            string vehicleId = "SELECT id as Id, registration as VehicleRegistration, color as VehicleColor FROM vehicle WHERE id = (SELECT vehicle FROM booking WHERE id = @id)";
            string userId = "SELECT id as Id, email as Email, name as Name, state as State FROM user WHERE id = (SELECT user FROM booking WHERE id = @id)";

            booking.VehicleResponse = connection.QuerySingleOrDefault<VehicleResponse>(vehicleId, new { id = id.IdValue });
            booking.UserResponse = connection.QuerySingleOrDefault<UserResponse>(userId, new { id = id.IdValue });

            return booking;
        }
        
    }

    public void Save(Booking booking)
    {
        using (var connection = new MySqlConnection(_connection.ConnectionString))
        {
            connection.Open();
            string query = "INSERT INTO booking (id, bookingdate, status, vehicle, user) VALUES (@id,@date,@status,@vehicle,@user)";
            connection.Execute(query, new {id = booking.Id.IdValue, date =booking.Date.DateValue, 
                status = booking.Status.StatusValue, vehicle = booking.VehicleResponse.Id, user = booking.UserResponse.Id});
        }
        
    }

    public void Delete(BookingId bookingId)
    {
        using (var connection = new MySqlConnection(_connection.ConnectionString))
        {
            connection.Open();
            string query = "DELETE FROM booking WHERE id = @Id";
            connection.Execute(query, new {Id = bookingId.IdValue});
        }
    }

    public void Patch(BookingStatus bookingStatus, BookingId bookingId)
    {
        using (var connection = new MySqlConnection(_connection.ConnectionString))
        {
            connection.Open();
            string query = "UPDATE booking SET status = @Status WHERE id = @Id";    
            connection.Execute(query, new{Status = bookingStatus.StatusValue, Id = bookingId.IdValue});
        }
    }
}