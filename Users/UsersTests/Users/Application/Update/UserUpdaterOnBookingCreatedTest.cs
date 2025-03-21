﻿/*using Cojali.Shared.Domain.Bus.Event;
using UsersManagement.Users.Application.Find;
using UsersManagement.Users.Application.Update;
using UsersManagement.Users.Domain;
using UsersTests.Users.Domain;

namespace UsersTests.Users.Application.Update;

public class UserUpdaterOnBookingCreatedTest : UserModuleApplicationUnitTestCase
{
    private readonly UserUpdaterOnBookingCreated userUpdaterOnBookingCreated;
    private readonly UserUpdater updater;
    private readonly UserFinder userFinder;

    public UserUpdaterOnBookingCreatedTest()
    {
        userFinder = new UserFinder(this.UserRepository.Object);
        this.updater = new UserUpdater(this.UserRepository.Object,
            this.userFinder, this.EventBus.Object, this.QueryBus.Object);
        this.userUpdaterOnBookingCreated = new UserUpdaterOnBookingCreated(this.updater, this.userFinder);
    }

    [Fact]
    public async Task ShouldUpdateUser_WhenBookingCreatedEventIsPublished()
    {
        // GIVEN
        Booking booking = BookingMother.CreateRandom();
        Vehicle vehicle = VehicleMother.CreateRandom();
        Usuario user = UserMother.CreateRandom();
        BookingCreated bookingCreated = BookingCreated.Create(
            booking.Id.IdValue,
            booking.Date.DateValue,
            vehicle.Id.IdValue,
            user.Id.Id,
            user.Email.Email,
            vehicle.VehicleRegistration.RegistrationValue,
            user.Name.Name);
        this.ShouldFindUser(user.Id, user);
        this.ShouldFindVehicle(vehicle.Id, vehicle);
        // WHEN
        await userUpdaterOnBookingCreated.OnAsync((DomainEvent) bookingCreated);
        // THEN
        this.ShouldHaveCalledUpdateWithCorrectParametersOnce(user);
    }
}*/