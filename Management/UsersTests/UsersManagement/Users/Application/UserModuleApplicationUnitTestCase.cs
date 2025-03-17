using Cojali.Shared.Domain.Bus.Command;
using Cojali.Shared.Domain.Bus.Event;
using Cojali.Shared.Domain.Bus.Query;
using Moq;
using UsersManagement.Senders.Domain;
using UsersManagement.Shared.Users.Domain.DomainEvents;
using UsersManagement.Shared.Users.Domain.Exceptions;
using UsersManagement.Shared.Vehicles.Domain.Commands;
using UsersManagement.Shared.Vehicles.Domain.Exceptions;
using UsersManagement.Shared.Vehicles.Domain.Querys;
using UsersManagement.Shared.Vehicles.Domain.Responses;
using UsersManagement.Users.Domain;
using UsersManagement.Users.Domain.ValueObject;
using UsersManagement.Vehicles.Application.Find;
using UsersManagement.Vehicles.Domain;
using UsersManagement.Vehicles.Domain.ValueObject;

namespace UsersTests.UsersManagement.Users.Application;

public class UserModuleApplicationUnitTestCase
{
    protected UserModuleApplicationUnitTestCase()
    {
        this.UserRepository = new Mock<IUserRepository>();
        this.SendRepository = new Mock<ISendRepository>();
        this.VehicleRepository = new Mock<IVehicleRepository>();
        this.EventBus = new Mock<IEventBus>();
        this.QueryBus = new Mock<IQueryBus>();
        this.CommandBus = new Mock<ICommandBus>();
    }
    protected Mock<IUserRepository> UserRepository { get; }
    protected Mock<ISendRepository> SendRepository { get; }
    protected Mock<IQueryBus> QueryBus { get; }
    protected Mock<IVehicleRepository> VehicleRepository { get; }
    protected Mock<IEventBus> EventBus { get; }
    
    protected Mock<ICommandBus> CommandBus { get; }

    protected void ShouldFindUser(UserId userId, Usuario user)
    {
        this.UserRepository.Setup(x => x.Find(userId)).Returns(user);
    }

    protected void ShouldFindVehicle(VehicleId vehicleId, Vehicle vehicle)
    {
        this.VehicleRepository.Setup(repo => repo.GetById(vehicleId)).Returns(vehicle);
    }

    protected void ShouldSearchUsers(IEnumerable<Usuario> usersList)
    {
        this.UserRepository.Setup(x => x.SearchUsers()).ReturnsAsync(usersList);
    }

    protected void ShouldSaveUsers(Usuario user)
    {
        this.UserRepository.Setup(x => x.Save(user));
    }

    protected void ShouldSendEmail(Send send)
    {
        this.SendRepository.Setup(x => x.Send(send));
    }

    protected void ShouldUpdate(Usuario user)
    {
        this.UserRepository.Setup(x => x.Update(user));
    }
    
    protected void ShouldThrowVehicleNotFoundExceptionInFirstCallAndReturnsVehicleInSecondCall(VehicleResponse vehicleResponse)
    {
        var sequence = new MockSequence();
        this.QueryBus
            .InSequence(sequence)
            .Setup(q => q.AskAsync(It.IsAny<VehicleFinderQuery>())).ThrowsAsync(new VehicleNotFoundException());
        
        this.QueryBus
            .InSequence(sequence)
            .Setup(q => q.AskAsync(It.IsAny<VehicleFinderQuery>())).ReturnsAsync(vehicleResponse);
    }

    protected void ShouldHaveCalledFinderWithCorrectParametersOnce(UserId id)
    {
        this.UserRepository.Verify(x => x.Find(id), Times.Once);
    }

    protected void ShouldHaveCalledUpdateWithCorrectParametersOnce(Usuario user)
    {
        this.UserRepository.Verify(x => x.Update(user), Times.Once);
    }

    protected void ShouldHaveCalledSaveWithCorrectParametersOnce(Usuario user)
    {
        this.UserRepository.Verify(x => x.Save(user), Times.Once);
    }

    protected void ShouldHaveCalledSenderWithCorrectParametersOnce(Send send)
    {
        this.SendRepository.Verify(x => x.Send(send), Times.Once);
    }

    protected void ShouldHaveCalledPublishWithcorrectParametersOnce<T>() where T : DomainEvent
    {
        this.EventBus.Verify(x => x.PublishAsync(It.Is<DomainEvent[]>
            (e => e.Any(ev => ev is T))), Times.Once);
    }

    protected void ShouldHaveCalledAskAsyncWithCorrectParametersOnce(VehicleFinderQuery query)
    {
        this.QueryBus.Verify(q => q.AskAsync(query), Times.Once);
    }

    protected void ShouldHaveCalledDispatchAsyncWithCorrectParametersOnce(VehicleCreatorCommand command)
    {
        this.CommandBus.Verify(c => c.DispatchAsync(command), Times.Once);
    }

    
}