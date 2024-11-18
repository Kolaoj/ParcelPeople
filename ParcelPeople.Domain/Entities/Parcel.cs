using ParcelPeople.Domain.Enums;

namespace ParcelPeople.Domain.Entities
{
    public class Parcel
    {
        public Guid Id { get; set; }
        public Guid ShipmentId { get; set; }
        public required ParcelTypes Type { get; set; }
        public double? Dimensions { get; set; } // Cubic inches
    }
}
