using Microsoft.EntityFrameworkCore;
using ParcelPeople.Domain.Entities;
using ParcelPeople.Infrastructure.DbContexts;
using ParcelPeople.Infrastructure.Repositories.Interfaces;

namespace ParcelPeople.Infrastructure.Repositories
{
    public class CityRepository(ShipmentDbContext context) : ICityRepository
    {
        private readonly ShipmentDbContext context = context ?? throw new ArgumentNullException(nameof(context));

        public async Task<City> GetCityById(int cityId) => await context.FindAsync<City>(cityId) ?? throw new Exception("City could not be found");

        public async Task<IEnumerable<City>> GetCitiesByIds(IEnumerable<int> cityIds)
        {
            return await context.Cities
                        .Where(city => cityIds.Contains(city.Id))
                        .AsNoTracking()
                        .ToListAsync();
        }

        public async Task<IEnumerable<City>> GetAllCities(int top = 10, int skip = 0)
        {
            return await context.Cities
                          .OrderBy(city => city.Name)
                          .Skip(skip)
                          .Take(top)
                          .AsNoTracking()
                          .ToListAsync();
        }
    }
}
