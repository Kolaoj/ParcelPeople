using ParcelPeople.Domain.Entities;

namespace ParcelPeople.Application.Services.Interfaces
{
    public interface ICityService
    {
        Task<IEnumerable<City>> GetAllCities(int top = 10, int skip = 0);
        Task<IEnumerable<City>> GetCitiesByIds(IEnumerable<int> cityIds);
        Task<City> GetCityById(int cityId);
    }
}
