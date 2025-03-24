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
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    
        var jsonVehicle = JsonSerializer.Deserialize<JsonElement>(json, options);

        if (TryGetProperty(jsonVehicle,"Id", out var idElement)
            && TryGetProperty(jsonVehicle,"Name", out var nameElement) 
            && TryGetProperty(jsonVehicle,"Email", out var emailElement)
            && TryGetProperty(jsonVehicle,"State", out var stateElement))
        {
            string? id = idElement.GetString();
            string? name = nameElement.GetString();
            string? email = emailElement.GetString();
            bool state = stateElement.GetBoolean();

            return Create(id, name,email, state);
        }

        return null;
    }
    
    private static bool TryGetProperty(JsonElement element, string propertyName, out JsonElement value)
    {
        // Intentar obtener la propiedad directamente (usando la configuración insensible a mayúsculas/minúsculas)
        if (element.TryGetProperty(propertyName, out value))
        {
            return true;
        }

        // Si no se encuentra, buscar en todas las propiedades de forma manual
        foreach (var property in element.EnumerateObject())
        {
            if (string.Equals(property.Name, propertyName, StringComparison.OrdinalIgnoreCase))
            {
                value = property.Value;
                return true;
            }
        }

        // No se encontró la propiedad
        value = default;
        return false;
    }
}