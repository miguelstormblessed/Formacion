﻿using Bogus;
using Users.Shared.Vehicles.Domain.Responses;
using Users.Users.Domain;
using Users.Users.Domain.ValueObject;
using UsersTests.Shared.Vehicles.Domain.Responses;

namespace UsersTests.Users.Domain;

public static class UserMother
{
    private static readonly Faker Faker = new Faker();

    public static Usuario CreateRandom()
    {
        UserId userId = UserId.Create(CreateRandomId());
        UserName userName = UserName.Create(CreateRandomUsername());
        UserEmail userEmail = UserEmail.Create(CreateRandomEmail());
        VehicleResponse vehicle = VehicleResponseMother.CreateRandom();
        return Usuario.CreateActivatedUserMother(userId, userName, userEmail, vehicle);
    }
    
    private static string CreateRandomId()
    {
        return Guid.NewGuid().ToString();
    }

    private static string CreateRandomUsername()
    {
        return Faker.Random.AlphaNumeric(10);
    }

    private static string CreateRandomEmail()
    {
        return Faker.Random.AlphaNumeric(10)+"@gmail.com";
    }
}