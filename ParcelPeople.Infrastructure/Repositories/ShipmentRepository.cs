using Microsoft.EntityFrameworkCore;
using ParcelPeople.Domain.Entities;
using ParcelPeople.Infrastructure.DbContexts;
using ParcelPeople.Infrastructure.Repositories.Interfaces;

namespace ParcelPeople.Infrastructure.Repositories
{
    public class ShipmentRepository(ShipmentDbContext context) : IShipmentRepository
    {
        private readonly ShipmentDbContext context = context ?? throw new ArgumentNullException(nameof(context));

        public async Task<Shipment> Add(Shipment shipment)
        {
            ArgumentNullException.ThrowIfNull(shipment);

            await context.Shipments.AddAsync(shipment);
            await context.SaveChangesAsync();

            return shipment;
        }

        public async Task<Shipment> GetById(Guid id)
        {
            return await context.Shipments
                .Include(e=> e.Cities)
                .Include(e => e.Parcels)
                .FirstOrDefaultAsync(e=> e.Id == id) ?? throw new Exception("Shipment could not be found");
        }

        public async Task<ShipmentCity> GetShipmentCityById(Guid id) => await context.FindAsync<ShipmentCity>(id) ?? throw new Exception("ShipmentCity could not be found");

        public async Task Update(Shipment shipment)
        {
            ArgumentNullException.ThrowIfNull(shipment);

            context.Shipments.Update(shipment);
            await context.SaveChangesAsync();
        }

        public async Task UpdateShipmentCity(ShipmentCity shipmentCity)
        {
            ArgumentNullException.ThrowIfNull(shipmentCity);

            context.ShipmentCities.Update(shipmentCity);
            await context.SaveChangesAsync();
        }
    }
}
