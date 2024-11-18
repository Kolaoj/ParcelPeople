using ParcelPeople.Application.Dtos.Add;
using ParcelPeople.Application.Dtos.Update;
using ParcelPeople.Domain.Entities;
using ParcelPeople.Domain.Enums;

namespace ParcelPeople.Application.Services.Interfaces
{
    public interface IShipmentService
    {
        Task<Shipment> CreateQuote(CreateShipmentQuote createQuote);
        Task CreateShipment(Guid shipmentId);
        Task MoveShipment(Guid shipmentCityId, MoveShipmentCity moveShipmentCity);
        Task UpdateShipmentStatus(Guid shipmentId, ShipmentStatus shipmentStatus);
        Task<Shipment> GetShipment(Guid shipmentId);
    }
}
