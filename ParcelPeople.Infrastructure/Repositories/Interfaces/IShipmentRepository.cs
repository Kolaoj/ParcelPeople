using ParcelPeople.Domain.Entities;

namespace ParcelPeople.Infrastructure.Repositories.Interfaces
{
    public interface IShipmentRepository
    {
        Task<Shipment> Add(Shipment shipment);
        Task<Shipment> GetById(Guid id);
        Task<ShipmentCity> GetShipmentCityById(Guid id);
        Task Update(Shipment shipment);
        Task UpdateShipmentCity(ShipmentCity shipmentCity);
    }
}
