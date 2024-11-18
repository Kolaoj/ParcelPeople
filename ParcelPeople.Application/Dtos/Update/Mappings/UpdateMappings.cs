using ParcelPeople.Domain.Entities;

namespace ParcelPeople.Application.Dtos.Update.Mappings
{
    public static class UpdateMappings
    {
        public static void UpdateShipmentCity(this ShipmentCity shipmentCity, MoveShipmentCity moveShipmentCity)
        {
            shipmentCity.TimeOfArrival = moveShipmentCity.TimeOfArrival;
            shipmentCity.Status = moveShipmentCity.Status;
        }
    }
}
