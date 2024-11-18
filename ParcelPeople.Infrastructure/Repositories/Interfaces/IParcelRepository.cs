using ParcelPeople.Domain.Entities;

namespace ParcelPeople.Infrastructure.Repositories.Interfaces
{
    public interface IParcelRepository
    {
        Task<ParcelSurcharge> GetParcelSurcharge(double dimensions);
    }
}
