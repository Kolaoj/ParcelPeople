using ParcelPeople.Domain.Enums;

namespace ParcelPeople.Application.Dtos.Update
{
    public class MoveShipmentCity
    {
        public Guid ShipmentCityId { get; set; }
        public DateTimeOffset? TimeOfArrival { get; set; }
        public ShipmentCityStatus Status { get; set; }
    }
}
