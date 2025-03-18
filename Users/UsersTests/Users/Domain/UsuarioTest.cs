using Cojali.Shared.Domain.Bus.Event;
using FluentAssertions;
using UsersManagement.Shared.Users.Domain.DomainEvents;
using UsersManagement.Shared.Vehicles.Domain.Responses;
using UsersManagement.Users.Domain;
using UsersManagement.Users.Domain.ValueObject;
using UsersManagement.Vehicles.Domain;
using UsersManagement.Vehicles.Domain.ValueObject;
using UsersTests.UsersManagement.Shared.Vehicles.Domain.Responses;
using UsersTests.UsersManagement.Vehicles.Domain;

namespace UsersTests.UsersManagement.Users.Domain;

public class UsuarioTest
{
    [Fact]
    public void ShouldInicialitePropertiesCorrectly_WhenUserIsNotActive()
    {
        // GIVEN
        Usuario user = UserMother.CreateRandom();
        VehicleResponse vehicle = VehicleResponseMother.CreateRandom();
        // WHEN
        Usuario userDeactivatedToTest = Usuario.CreateUserDeactivated(
            user.Id, user.Name, user.Email, vehicle);
        //THEN
        userDeactivatedToTest.Should().NotBeNull();
        userDeactivatedToTest.Id.Should().Be(user.Id);
        userDeactivatedToTest.Name.Should().Be(user.Name);
        userDeactivatedToTest.Email.Should().Be(user.Email);
        userDeactivatedToTest.State.Should().NotBe(user.State);
    }

    [Fact]
    public void ShouldInicialitePropertiesCorrectly_WhenUserIsActive()
    {
        // GIVEN
        Usuario user = UserMother.CreateRandom();
        VehicleResponse vehicle = VehicleResponseMother.CreateRandom();
        // WHEN
        Usuario userActivatedToTest = Usuario.CreateUserActivated(
            user.Id, user.Name, user.Email,vehicle);
        //THEN
        userActivatedToTest.Should().NotBeNull();
        userActivatedToTest.Id.Should().Be(user.Id);
        userActivatedToTest.Name.Should().Be(user.Name);
        userActivatedToTest.Email.Should().Be(user.Email);
        userActivatedToTest.State.Should().Be(user.State);
    }

    [Fact]
    public void ShouldInicialiteUserMotherPropertiesCorrectly_WhenUserIsActive()
    {
        // GIVEN
        Usuario user = UserMother.CreateRandom();
        VehicleResponse vehicle = VehicleResponseMother.CreateRandom();
        // WHEN
        Usuario userActivatedToTest = Usuario.CreateActivatedUserMother(
            user.Id, user.Name, user.Email,vehicle);
        //THEN
        userActivatedToTest.Should().NotBeNull();
        userActivatedToTest.Id.Should().Be(user.Id);
        userActivatedToTest.Name.Should().Be(user.Name);
        userActivatedToTest.Email.Should().Be(user.Email);
        userActivatedToTest.State.Should().Be(user.State);
    }

    [Fact]
    public void ShouldInicialiteUserMotherPropertiesCorrectly_WhenUserIsInactive()
    {
        // GIVEN
        Usuario user = UserMother.CreateRandom();
        VehicleResponse vehicle = VehicleResponseMother.CreateRandom();
        // WHEN
        Usuario userDeactivatedToTest = Usuario.CreateUserDeactivated(
            user.Id, user.Name, user.Email, vehicle);
        //THEN
        userDeactivatedToTest.Should().NotBeNull();
        userDeactivatedToTest.Id.Should().Be(user.Id);
        userDeactivatedToTest.Name.Should().Be(user.Name);
        userDeactivatedToTest.Email.Should().Be(user.Email);
        userDeactivatedToTest.State.Should().NotBe(user.State);
    }

    [Fact]
    public void ShouldUpdatePropertiesCorrectly()
    {
        // GIVEN
        Usuario user = UserMother.CreateRandom();
        Usuario newUser = UserMother.CreateRandom();
        VehicleResponse vehicle = VehicleResponseMother.CreateRandom();
        // WHEN
        user.Update(newUser.Id, newUser.Name, newUser.Email, newUser.State, vehicle);
        // THEN
        user.Id.Should().Be(newUser.Id);
        user.Name.Should().Be(newUser.Name);
        user.Email.Should().Be(newUser.Email);
        user.State.Should().Be(newUser.State);
        user.Vehicle.Should().BeEquivalentTo(vehicle);
    }

    [Fact]
    public void ShouldDeleteCorrectly()
    {
        // GIVEN
        Usuario user = UserMother.CreateRandom();
        // THEN
        user.Delete(user.Id, user.Name, user.Email);
        // WHEN
        user.State.Active.Should().Be(false);
    }

    [Fact]
    public void ShouldRecordEvent_WhenCreateActiveUser()
    {
        // GIVEN
        Usuario userTest = UserMother.CreateRandom();
        VehicleResponse vehicle = VehicleResponseMother.CreateRandom();
        // WHEN
        Usuario user = Usuario.CreateUserActivated(userTest.Id, userTest.Name, userTest.Email, vehicle);
        DomainEvent[]? domanEvents = user.PullDomainEvents();
        // THEN
        domanEvents.Should().ContainSingle();
        domanEvents.Should().Contain(e => e is ActivatedUserCreated);
    }
    [Fact]
    public void ShouldRecordEvent_WhenCreateInactiveUser()
    {
        // GIVEN
        Usuario userTest = UserMother.CreateRandom();
        VehicleResponse vehicle = VehicleResponseMother.CreateRandom();
        // WHEN
        Usuario user = Usuario.CreateUserDeactivated(userTest.Id, userTest.Name, userTest.Email, vehicle);
        DomainEvent[]? domanEvents = user.PullDomainEvents();
        // THEN
        domanEvents.Should().ContainSingle();
        domanEvents.Should().Contain(e => e is InactiveUserCreated);
    }

    [Fact]
    public void ShouldRecordEvent_WhenUpdateUser()
    {
        // GIVEN
        Usuario user = UserMother.CreateRandom();
        Usuario newUser = UserMother.CreateRandom();
        VehicleResponse vehicle = VehicleResponseMother.CreateRandom();
        // WHEN
        user.Update(newUser.Id, newUser.Name, newUser.Email, newUser.State, vehicle);
        DomainEvent[]? domanEvents = user.PullDomainEvents();
        domanEvents.Should().ContainSingle();
        domanEvents.Should().Contain(e => e is UserUpdated);
        
    }

    [Fact]
    public void ShouldRecordEvent_WhenDeleteUser()
    {
        // GIVEN
        Usuario user = UserMother.CreateRandom();
        // WHEN
        user.Delete(user.Id, user.Name, user.Email);
        DomainEvent[]? domanEvents = user.PullDomainEvents();
        // THEN
        domanEvents.Should().ContainSingle();
        domanEvents.Should().Contain(e => e is UserDeleted);
    }

    [Fact]
    public void ShouldBeEquivalents()
    {
        // Comprueba el método equals
        // GIVEN
        Usuario user = UserMother.CreateRandom();
        VehicleResponse vehicle = VehicleResponseMother.CreateRandom();
        // WHEN
        Usuario user1 = Usuario.CreateUserActivated(user.Id, user.Name, user.Email,vehicle);
        Usuario user2 = Usuario.CreateUserActivated(user.Id, user.Name, user.Email,vehicle);
        // THEN
        user1.Should().Be(user2);
    }
    
}