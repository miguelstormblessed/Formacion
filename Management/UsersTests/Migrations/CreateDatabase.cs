using FluentMigrator;
using UsersManagement.Bookings.Domain.ValueObject;
using UsersManagement.Shared.Users.Domain.Responses;
using UsersTests.UsersManagement.Shared.Users.Responses;

namespace UsersTests.Migrations;
[Migration(2983849839)]
public class CreateDatabase : Migration
{
    public override void Up()
    {
        this.Create.Table("user")
            .WithColumn("id").AsString(36).NotNullable().PrimaryKey()
            .WithColumn("email").AsString()
            .WithColumn("name").AsString()
            .WithColumn("state").AsBoolean()
            .WithColumn("vehicles").AsString();

        this.Create.Table("vehicle")
            .WithColumn("id").AsString(36).NotNullable().PrimaryKey()
            .WithColumn("registration").AsString()
            .WithColumn("color").AsString();

        this.Create.Table("booking")
            .WithColumn("id").AsString(36).NotNullable().PrimaryKey()
            .WithColumn("bookingdate").AsString()
            .WithColumn("status").AsBoolean()
            .WithColumn("vehicle").AsString(36).NotNullable()
            .WithColumn("user").AsString(36).NotNullable();

        this.Create.Table("count")
            .WithColumn("id").AsString(36).NotNullable().PrimaryKey()
            .WithColumn("activeuser").AsInt32()
            .WithColumn("inactiveuser").AsInt32();
        
        this.Insert.IntoTable("count").Row(new { id = "6a853495-b793-4066-884e-b8ea4751ead8", activeuser = 30, inactiveuser=30 });

        this.Insert.IntoTable("user").Row(new { id = "0babdeec-c946-4042-a2cf-c2b452d5176d", name = "ñalsdjkf", email = "añlsdf@mail", state = true, vehicles = "" });
        this.Insert.IntoTable("vehicle").Row(new
            { id = "28548eac-8829-4275-b336-078e00e96f56", registration = "asñfj-001", color = "Red" });
        this.Insert.IntoTable("booking").Row(new
        {
            id = "4902524d-f374-4a5d-bf7a-e1b008485e84",
            bookingdate = "13/07/2030",
            status = false,
            vehicle = "28548eac-8829-4275-b336-078e00e96f56",
            user = "0babdeec-c946-4042-a2cf-c2b452d5176d"
        });
    }

    public override void Down()
    {
    }
}