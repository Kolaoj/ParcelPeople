using ParcelPeople.Application.Services.Interfaces;
using ParcelPeople.Infrastructure.Repositories.Interfaces;

namespace ParcelPeople.Application.Services
{
    public class ParcelService(IParcelRepository parcelRepository) : IParcelService
    {
        private readonly IParcelRepository parcelRepository = parcelRepository ?? throw new ArgumentNullException(nameof(parcelRepository));

        public async Task<decimal> GetTotalSurcharge(IEnumerable<double> parcelsDimensions)
        {
            var parcelSurchargeTasks = parcelsDimensions
                .Select(parcelRepository.GetParcelSurcharge);

            var parcelSurcharges = await Task.WhenAll(parcelSurchargeTasks);

            var totalCost = parcelSurcharges.Sum(ps => ps.Surcharge);

            return totalCost;
        }
    }
}
