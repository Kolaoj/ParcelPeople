using ParcelPeople.Domain.Enums;

namespace ParcelPeople.Domain.Entities
{
    public class Shipment
    {
        public Guid Id { get; set; }
        public required Guid CustomerId { get; set; }
        public required string ReceiverName { get; set; }
        public required ShipmentStatus Status { get; set; }
        public required ICollection<ShipmentCity> Cities { get; set; }
        public required ICollection<Parcel> Parcels { get; set; }
        public required decimal Cost { get; set; }
        public required string CostDisplayed { get; set; }
    }
}
