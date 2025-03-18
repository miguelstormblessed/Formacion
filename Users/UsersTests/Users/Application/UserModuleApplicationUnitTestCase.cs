using Bogus.DataSets;
using Cojali.Shared.Domain.Bus.Command;
using Cojali.Shared.Domain.Bus.Event;
using Cojali.Shared.Domain.Bus.Query;
using Moq;
using Users.Shared.HttpClient;
using Users.Shared.Vehicles.Domain.Commands;
using Users.Shared.Vehicles.Domain.Exceptions;
using Users.Shared.Vehicles.Domain.Querys;
using Users.Shared.Vehicles.Domain.Responses;
using UsersManagement.Users.Domain;
using UsersManagement.Users.Domain.ValueObject;

namespace UsersTests.Users.Application;

public class UserModuleApplicationUnitTestCase
{
    protected UserModuleApplicationUnitTestCase()
    {
        this.UserRepository = new Mock<IUserRepository>();
        this.EventBus = new Mock<IEventBus>();
        this.QueryBus = new Mock<IQueryBus>();
        this.CommandBus = new Mock<ICommandBus>();
        this.HttpClientService = new Mock<IHttpClientService>();
    }
    protected Mock<IUserRepository> UserRepository { get; }
    protected Mock<IQueryBus> QueryBus { get; }
    protected Mock<IEventBus> EventBus { get; }
    protected Mock<IHttpClientService> HttpClientService { get; }
    protected Mock<ICommandBus> CommandBus { get; }

    protected void ShouldFindUser(UserId userId, Usuario user)
    {
        this.UserRepository.Setup(x => x.Find(userId)).Returns(user);
    }

    protected void ShouldFindVehicleByHttp(HttpResponseMessage message)
    {
        HttpClientService.Setup(h => h.GetAsync(It.IsAny<string>())).ReturnsAsync(message);

    }

    protected void ShouldSearchUsers(IEnumerable<Usuario> usersList)
    {
        this.UserRepository.Setup(x => x.SearchUsers()).ReturnsAsync(usersList);
    }

    protected void ShouldSaveUsers(Usuario user)
    {
        this.UserRepository.Setup(x => x.Save(user));
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

    protected void ShouldHaveCalledGetAsyncWithCorrectParametersOnce()
    {
        HttpClientService.Verify(c => c.GetAsync(It.IsAny<string>()), Times.Once);
    }

    
}