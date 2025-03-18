using System.Text.Json;
using UsersManagement.Shared.Vehicles.Domain.Responses;

namespace UsersManagement.Shared.Users.Domain.Responses;

public class UserResponse : IEquatable<UserResponse>
{
    private UserResponse(string id, string name, string email, bool state)
    {
        Id = id;
        Name = name;
        Email = email;
        State = state;
    }
    
    public string Id { get;  set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public bool State { get; set; }

    public static UserResponse Create(string id, string name, string email, bool state)
    {
        return new UserResponse(id, name, email, state);
    }
    
    protected UserResponse(){}

    public bool Equals(UserResponse? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id == other.Id && Name == other.Name && Email == other.Email && State == other.State;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((UserResponse)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name, Email, State);
    }

    public  string ToJson()
    {
        return JsonSerializer.Serialize(this);
    }
    
    public static UserResponse FromJson(string json)
    {
        var jsonVehicle = JsonSerializer.Deserialize<JsonElement>(json/*, options*/);

        if (jsonVehicle.TryGetProperty("Id", out var idElement)
            && jsonVehicle.TryGetProperty("Name", out var nameElement) 
            && jsonVehicle.TryGetProperty("Email", out var emailElement)
            && jsonVehicle.TryGetProperty("State", out var stateElement))
        {
            string? id = idElement.GetString();
            string? name = nameElement.GetString();
            string? email = emailElement.GetString();
            bool state = stateElement.GetBoolean();

            return Create(id, name,email, state);
        }

        return null;
    }
}