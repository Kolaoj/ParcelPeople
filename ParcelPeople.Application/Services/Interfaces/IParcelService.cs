namespace ParcelPeople.Application.Services.Interfaces
{
    public interface IParcelService
    {
        Task<decimal> GetTotalSurcharge(IEnumerable<double> parcelsDimensions);
    }
}
