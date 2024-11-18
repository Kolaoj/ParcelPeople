using ParcelPeople.Domain.Entities;

namespace ParcelPeople.Infrastructure.Repositories.Interfaces
{
    public interface ICityRepository
    {
        Task<IEnumerable<City>> GetAllCities(int top, int skip);
        Task<IEnumerable<City>> GetCitiesByIds(IEnumerable<int> cityIds);
        Task<City> GetCityById(int cityId);
    }
}
