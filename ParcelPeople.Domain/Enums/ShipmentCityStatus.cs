using System.Text.Json.Serialization;

namespace ParcelPeople.Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter<ShipmentCityStatus>))]
    public enum ShipmentCityStatus
    {
        Awaiting,
        Approaching,
        Arrived,
        Departed
    }
}
