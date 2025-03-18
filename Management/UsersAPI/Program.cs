using System.Data;
using System.Reflection;
using Cojali.Shared.App.Extension.DependencyInjection.AutoRegister;
using Cojali.Shared.Domain.Bus.Command;
using Cojali.Shared.Domain.Bus.Event;
using Cojali.Shared.Domain.Bus.Query;
using Cojali.Shared.Infrastructure.Bus.Extensions;
using Cojali.Shared.Infrastructure.Bus.Memory;
using Dapper;
using Microsoft.AspNetCore.Mvc.Controllers;
using MySqlConnector;
using UsersManagement.Bookings.Application.Create;
using UsersManagement.Bookings.Application.Delete;
using UsersManagement.Bookings.Application.Find;
using UsersManagement.Bookings.Application.Search;
using UsersManagement.Bookings.Domain;
using UsersManagement.Bookings.Domain.ValueObject;
using UsersManagement.Bookings.Infrastructure;
using UsersManagement.Bookings.Infrastructure.Mappers;
using UsersManagement.Counters.Application.Finder;
using UsersManagement.Counters.Application.Updater;
using UsersManagement.Counters.Domain;
using UsersManagement.Counters.Domain.ValueObject;
using UsersManagement.Counters.Infrastucture;
using UsersManagement.Counters.Infrastucture.Mapper;
using UsersManagement.Senders.Application;
using UsersManagement.Senders.Domain;
using UsersManagement.Senders.Infraestructure;
using UsersManagement.Shared.HttpClient;
using UsersManagement.Shared.Users.Domain.DomainEvents;
using UsersManagement.Vehicles.Application.Create;
using UsersManagement.Vehicles.Application.Find;
using UsersManagement.Vehicles.Application.Search;
using UsersManagement.Vehicles.Domain;
using UsersManagement.Vehicles.Domain.ValueObject;
using UsersManagement.Vehicles.Infrastructure;
using UsersManagement.Vehicles.Infrastructure.Mappers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {

    //Configurar agrupación por etiquetas
        c.TagActionsBy(api =>
        {
            if (api.GroupName != null)
            {
                return new[] { api.GroupName };
            }

            if (api.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
            {
                return new[] { controllerActionDescriptor.ControllerName };
            }

            throw new InvalidOperationException("Unable to determine tag for endpoint");
        });

        c.DocInclusionPredicate((name, api) => true);
    
});


builder.Services.AddTransient<IDbConnection>(_ =>
    new MySqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));




/*SqlMapper.AddTypeHandler(typeof(UserId), new UserIdMapper());
SqlMapper.AddTypeHandler(typeof(UserName), new UserNameMapper());
SqlMapper.AddTypeHandler(typeof(UserEmail), new UserEmailMapper());
SqlMapper.AddTypeHandler(typeof(UserState), new UserStateMapper());*/
SqlMapper.AddTypeHandler(typeof(CountId), new CountMapper());
SqlMapper.AddTypeHandler(typeof(VehicleId), new VehicleIdMapper());
SqlMapper.AddTypeHandler(typeof(VehicleColor), new VehicleColorMapper());
SqlMapper.AddTypeHandler(typeof(VehicleRegistration), new VehicleRegistrationMapper());
SqlMapper.AddTypeHandler(typeof(BookingId), new BookingIdMapper());
SqlMapper.AddTypeHandler(typeof(BookingDate), new BookingDateMapper());
SqlMapper.AddTypeHandler(typeof(BookingStatus), new BookingStatusMapper());


builder.Services.AddScoped<ISendRepository, ConsoleSender>();
builder.Services.AddScoped<Sender>();

/*builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<UserActiveCreator>();
builder.Services.AddScoped<UserInactiveCreator>();
builder.Services.AddScoped<UserUpdater>();
builder.Services.AddScoped<UserSearcher>();
builder.Services.AddScoped<UserFinder>();
builder.Services.AddScoped<UserDeleter>();*/

builder.Services.AddScoped<ICountRepository, CountRepository>();
builder.Services.AddScoped<UsersCounterFinder>();
builder.Services.AddScoped<UsersCounterUpdater>();

builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<VehicleFinder>();
builder.Services.AddScoped<VehicleCreator>();
builder.Services.AddScoped<VehicleSearcher>();

builder.Services.AddScoped<HttpClient>();
builder.Services.AddScoped<IHttpClientService, HttpClientService>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<BookingCreator>();
builder.Services.AddScoped<BookingFinder>();
builder.Services.AddScoped<BookingSearcher>();
builder.Services.AddScoped<BookingSearcherByUser>();
builder.Services.AddScoped<BookingSearcherByVehicle>();
builder.Services.AddScoped<BookingDeleter>();


builder.Services.AddScoped<IEventBus, InMemoryApplicationEventBus>();
builder.Services.AddScoped<IQueryBus, InMemoryQueryBus>();
builder.Services.AddScoped<ICommandBus, InMemoryCommandBus>();

builder.Services.AddDomainEventSubscribersServices(Assembly.Load("UsersManagement"));
builder.Services.RegisterAssemblyPublicNonGenericClasses(Assembly.Load("UsersManagement"))
    .Where(type => type.GetTypeInfo().ImplementedInterfaces.Select(i => 
    i.GetTypeInfo()).Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IQueryHandler<,>))).AsPublicImplementedInterfaces();

builder.Services.RegisterAssemblyPublicNonGenericClasses(Assembly.Load("UsersManagement"))
    .Where(type => type.GetTypeInfo().ImplementedInterfaces.Select(i => i.GetTypeInfo())
        .Any(i => i.IsGenericType &&
                  i.GetGenericTypeDefinition() == typeof(ICommandHandler<>)))
    .AsPublicImplementedInterfaces();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseRouting();


app.MapControllers();


app.Run();

public partial class Program { }