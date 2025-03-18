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
        // var options = new JsonSerializerOptions();
        ////options.Converters.Add(new JsonStringEnumConverter());

        var jsonVehicle = JsonSerializer.Deserialize<JsonElement>(json/*, options*/);

        if (jsonVehicle.TryGetProperty("Id", out var idElement)
            && jsonVehicle.TryGetProperty("VehicleRegistration", out var registrationElement) 
            && jsonVehicle.TryGetProperty("VehicleColor", out var colorElement))
        {
            string? id = idElement.GetString();
            string? registration = registrationElement.GetString();
            string? color = colorElement.GetString();

            return Create(id, registration, color);
        }

        return null;
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