using FluentMigrator;

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
    }

    public override void Down()
    {
    }
}