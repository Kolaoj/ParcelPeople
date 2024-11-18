using System.ComponentModel.DataAnnotations;

namespace ParcelPeople.Domain.Entities
{
    public class ParcelSurcharge
    {
        [Key]
        public double DimensionThreshold { get; set; }
        public decimal Surcharge { get; set; }
    }
}
