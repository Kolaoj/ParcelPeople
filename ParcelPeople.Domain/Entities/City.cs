namespace ParcelPeople.Domain.Entities
{
    public class City
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string CultureCode { get; set; }
        public required string Country { get; set; }
        public required decimal OriginCost { get; set; }
    }
}
