using ParcelPeople.Application.Services.Interfaces;
using ParcelPeople.Domain.Entities;
using ParcelPeople.Infrastructure.Repositories.Interfaces;

namespace ParcelPeople.Application.Services
{
    public class CityService(ICityRepository cityRepository) : ICityService
    {
        private readonly ICityRepository cityRepository = cityRepository ?? throw new ArgumentNullException(nameof(cityRepository));

        public async Task<City> GetCityById(int cityId) => await cityRepository.GetCityById(cityId);
        
        public async Task<IEnumerable<City>> GetAllCities(int top = 10, int skip = 0) => await cityRepository.GetAllCities(top, skip);

        public async Task<IEnumerable<City>> GetCitiesByIds(IEnumerable<int> cityIds)
        {
            if (cityIds == null || !cityIds.Any())
            {
                throw new ArgumentException("City Ids must not be null or empty", nameof(cityIds));
            }

            return await cityRepository.GetCitiesByIds(cityIds);
        }
    }
}
