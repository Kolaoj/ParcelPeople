using ParcelPeople.Domain.Enums;

namespace ParcelPeople.Domain.Entities
{
    public class ShipmentCity
    {
        public Guid Id { get; set; }
        public Guid ShipmentId { get; set; }
        public int CityId { get; set; }
        public bool Origin { get; set; }
        public bool Destination { get; set; }
        public DateTimeOffset? TimeOfArrival { get; set; }
        public ShipmentCityStatus Status { get; set; } = ShipmentCityStatus.Awaiting;
    }
}
