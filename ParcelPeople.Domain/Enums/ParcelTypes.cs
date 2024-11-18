using System.Text.Json.Serialization;

namespace ParcelPeople.Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter<ParcelTypes>))]

    public enum ParcelTypes
    {
        Envelope,
        Package
    }
}
