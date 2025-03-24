using System.Text.Json;
using System.Text.Json.Serialization;

namespace Users.Shared.Vehicles.Domain.Responses;

public class VehicleResponse
{
    private VehicleResponse(string id, string vehicleVehicleRegistration, string vehicleColor)
    {
        Id = id;
        VehicleRegistration = vehicleVehicleRegistration;
        VehicleColor = vehicleColor;
    }
    protected VehicleResponse() { }
    
    public string Id { get; private set; }
    public string VehicleRegistration { get; private set; }
    public string VehicleColor  { get; private set; }

    public static VehicleResponse Create(string? id, string? vehicleRegistration, string? vehicleVehicleColor)
    {
        return new VehicleResponse(id, vehicleRegistration, vehicleVehicleColor);
    }
    
    // Serializar -> Convertir de objeto a JSON
    public string ToJson()
    {
        var options = new JsonSerializerOptions();
        options.Converters.Add(new JsonStringEnumConverter()); //Convierte num del enum en string (colores)
        return JsonSerializer.Serialize(this, options);
    }

    public static VehicleResponse? FromJson(string json)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    
        var jsonVehicle = JsonSerializer.Deserialize<JsonElement>(json, options);

        if (TryGetProperty(jsonVehicle, "Id", out var idElement)
            && TryGetProperty(jsonVehicle,"VehicleRegistration", out var registrationElement) 
            && TryGetProperty(jsonVehicle,"VehicleColor", out var colorElement))
        {
            string? id = idElement.GetString();
            string? registration = registrationElement.GetString();
            string? color = colorElement.GetString();

            return Create(id, registration, color);
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
    

    public bool Equals(VehicleResponse? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id.Equals(other.Id) && VehicleRegistration.Equals(other.VehicleRegistration) && VehicleColor == other.VehicleColor;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((VehicleResponse)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, VehicleRegistration, VehicleColor);
    }
}