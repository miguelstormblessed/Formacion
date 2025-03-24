using Cojali.Shared.Domain.Bus.Command;
using Cojali.Shared.Domain.Bus.Event;
using Cojali.Shared.Domain.Bus.Query;
using Moq;
using UsersManagement.Bookings.Domain;
using UsersManagement.Bookings.Domain.Specification;
using UsersManagement.Bookings.Domain.ValueObject;
using UsersManagement.Shared.HttpClient;
using UsersManagement.Shared.Users.Domain.Commands;
using UsersManagement.Shared.Users.Domain.Querys;
using UsersManagement.Shared.Users.Domain.Responses;
using UsersManagement.Shared.Vehicles.Domain.Querys;
using UsersManagement.Shared.Vehicles.Domain.Responses;

namespace UsersTests.UsersManagement.Bookings.Application;

public class BookingsModuleApplicationTestCase
{
    public BookingsModuleApplicationTestCase(){
        QueryBus = new Mock<IQueryBus>();
        EventBus = new Mock<IEventBus>();
        CommandBus = new Mock<ICommandBus>();
        BookingRepository = new Mock<IBookingRepository>();
        HttpClientService = new Mock<IHttpClientService>();
        //UserRepository = new Mock<IUserRepository>();
    }

    protected Mock<IQueryBus> QueryBus { get; set; }
    protected Mock<IEventBus> EventBus{ get; set; }
    protected Mock<ICommandBus> CommandBus{ get; set; }
    protected Mock<IBookingRepository> BookingRepository { get; set; }
    protected Mock<IHttpClientService> HttpClientService { get; set; }
    //protected Mock<IUserRepository> UserRepository { get; set; }

    /*protected void ShouldReturnVehicleResponseThenUserResponse(VehicleFinderQuery vehicleFinderQuery, VehicleResponse vehicleResponse, UserFinderQuery userFinderQuery, UserResponse userResponse)
    {
        var sequence = new MockSequence();
        this.QueryBus
            .InSequence(sequence)
            .Setup(q => q.AskAsync(vehicleFinderQuery)).ReturnsAsync(vehicleResponse);
        this.QueryBus
            .InSequence(sequence)
            .Setup(q => q.AskAsync(userFinderQuery)).ReturnsAsync(userResponse);
    }*/

    /*protected void ShouldFindUser(UserId id, Usuario user)
    {
        this.UserRepository.Setup(r => r.Find(id)).Returns(user);
    }*/

    protected void ShouldFindBooking(BookingId bookingId, Booking booking)
    {
        this.BookingRepository.Setup(b => b.GetBookingById(bookingId)).Returns(booking);
    }

    

    protected void ShouldFindVehicle(VehicleFinderQuery query, VehicleResponse response)
    {
        this.QueryBus.Setup(q => q.AskAsync(query)).ReturnsAsync(response);
    }
    
    protected void SholdHaveCalledAskAsyncWithCorrectVehicleFinderQueryParametersOnce(VehicleFinderQuery query)
    {
        this.QueryBus.Verify(q => q.AskAsync(query), Times.Once);
    }

    protected void ShouldHaveCalledAskAsyncWithCorrectUserFinderQueryParametersOnce(UserFinderQuery query)
    {
        this.QueryBus.Verify(q => q.AskAsync(query), Times.Once);
    }

    protected void ShouldHaveCalledSaveWithCorrectParametersOnce(Booking booking)
    {
        this.BookingRepository.Verify(q => q.Save(booking), Times.Once);
    }

    protected void ShouldHaveCalledPublishAsyncWithCorrectParametersOnce<T>() where T : DomainEvent
    {
        this.EventBus.Verify(x => x.PublishAsync(It.Is<DomainEvent[]>
            (e => e.Any(ev => ev is T))), Times.Once);
    }

    protected void ShouldHaveCalledGetByIdWithCorrectParametersOnce(BookingId bookingId)
    {
        this.BookingRepository.Verify(b => b.GetBookingById(bookingId), Times.Once);
    }

    protected void ShouldHaveCalledDeleteWithCorrectParametersOnce(BookingId bookingId)
    {
        this.BookingRepository.Verify(b => b.Delete(bookingId), Times.Once);
    }

    protected void ShouldHaveCalledGetAllBookingsByUserIdWithCorrectParametersOnce(string id)
    {
        this.BookingRepository.Verify(b => b.Search(It.IsAny<BookingByUserIdSpecification>()), Times.Once);
    }

    protected void ShouldHaveCalledGetAllBookingsByVehicleWithCorrectParametersOnce(string vehicleId)
    {
        this.BookingRepository.Verify(b => b.Search(It.IsAny<BookingByVehicleIdSpecification>()), Times.Once);
    }

    protected void ShouldHaveCalledAskAsyncWithCorrectVehicleFinderQueryParametersOnce(VehicleFinderQuery query)
    {
        this.QueryBus.Verify(q => q.AskAsync(query), Times.Once);
    }

    protected void ShouldHaveCalledGetAllBookingsWithCorrectParametersOnce()
    {
        this.BookingRepository.Verify(b => b.SearchAll(), Times.Once);
    }

    protected void ShouldHaveCalledDispatchAsyncWithCorrectParametersOnce(UserUpdaterCommand command)
    {
        this.CommandBus.Verify(c => c.DispatchAsync(command), Times.Once);
    }

    protected void ShouldHaveCalledPatchWithCorrectParametersOnce(BookingStatus status, BookingId bookingId)
    {
        this.BookingRepository.Verify(b => b.Patch(status, bookingId), Times.Once);
    }
}