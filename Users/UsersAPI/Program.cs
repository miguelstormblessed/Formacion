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
using MySql.Data.MySqlClient;
using Users.Shared.HttpClient;
using Users.Shared.Users.Domain.DomainEvents;
using UsersManagement.Users.Application.Create;
using UsersManagement.Users.Application.Delete;
using UsersManagement.Users.Application.Find;
using UsersManagement.Users.Application.Search;
using UsersManagement.Users.Application.Update;
using UsersManagement.Users.Domain;
using UsersManagement.Users.Domain.ValueObject;
using UsersManagement.Users.Infrastructure;
using UsersManagement.Users.Infrastructure.Mappers;

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
builder.Services.AddScoped<HttpClient>();
builder.Services.AddScoped<IHttpClientService, HttpClientService>();
SqlMapper.AddTypeHandler(typeof(UserId), new UserIdMapper());
SqlMapper.AddTypeHandler(typeof(UserName), new UserNameMapper());
SqlMapper.AddTypeHandler(typeof(UserEmail), new UserEmailMapper());
SqlMapper.AddTypeHandler(typeof(UserState), new UserStateMapper());

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<UserActiveCreator>();
builder.Services.AddScoped<UserInactiveCreator>();
builder.Services.AddScoped<UserUpdater>();
builder.Services.AddScoped<UserSearcher>();
builder.Services.AddScoped<UserFinder>();
builder.Services.AddScoped<UserDeleter>();


builder.Services.AddScoped<IEventBus, InMemoryApplicationEventBus>();
builder.Services.AddScoped<IQueryBus, InMemoryQueryBus>();
builder.Services.AddScoped<ICommandBus, InMemoryCommandBus>();

builder.Services.AddDomainEventSubscribersServices(Assembly.GetAssembly(typeof(Usuario)));
builder.Services.RegisterAssemblyPublicNonGenericClasses(typeof(ActivatedUserCreated).Assembly)
    .Where(type => type.GetTypeInfo().ImplementedInterfaces.Select(i => 
        i.GetTypeInfo()).Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IQueryHandler<,>))).AsPublicImplementedInterfaces();

builder.Services.RegisterAssemblyPublicNonGenericClasses(Assembly.GetAssembly(typeof(Usuario)))
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

