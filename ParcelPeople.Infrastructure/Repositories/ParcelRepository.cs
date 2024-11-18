using Microsoft.EntityFrameworkCore;
using ParcelPeople.Domain.Entities;
using ParcelPeople.Infrastructure.DbContexts;
using ParcelPeople.Infrastructure.Repositories.Interfaces;

namespace ParcelPeople.Infrastructure.Repositories
{
    public class ParcelRepository(ShipmentDbContext context) : IParcelRepository
    {
        private readonly ShipmentDbContext context = context ?? throw new ArgumentNullException(nameof(context));

        public async Task<ParcelSurcharge> GetParcelSurcharge(double dimensions)
        {
            var parcelSurcharges = await context.ParcelSurcharges
              .Where(surcharge => surcharge.DimensionThreshold <= dimensions)
              .AsNoTracking()
              .ToListAsync();

            return parcelSurcharges.OrderByDescending(s => s.Surcharge).First();

        }
    }
}
