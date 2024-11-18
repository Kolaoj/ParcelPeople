using ParcelPeople.Domain.Enums;

namespace ParcelPeople.Application.Dtos.Add
{
    public class CreateParcel
    {
        public required ParcelTypes Type { get; set; }
        public double? Dimensions { get; set; } // Cubic inches
    }
}
