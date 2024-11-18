using System.Text.Json.Serialization;

namespace ParcelPeople.Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter<ShipmentStatus>))]

    public enum ShipmentStatus
    {
        Quote,           // The shipment has been quoted but not yet created.
        Pending,         // The shipment is created but not yet processed.
        Processed,       // The shipment has been processed and is ready for dispatch.
        InTransit,       // The shipment is currently in transit.
        Delivered,       // The shipment has been successfully delivered.
        Collected,       // The shipment has been successfully delivered.
        Delayed,         // The shipment is delayed due to unforeseen circumstances.
        Returned,        // The shipment has been returned to the sender.
        Canceled,        // The shipment was canceled before being processed.
        Lost             // The shipment was lost and is untraceable.
    }
}
