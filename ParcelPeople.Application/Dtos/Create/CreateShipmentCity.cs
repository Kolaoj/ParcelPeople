namespace ParcelPeople.Application.Dtos.Add
{
    public class CreateShipmentCity
    {
        public bool Origin { get; set; }
        public bool Destination { get; set; }
        public required int CityId { get; set; }
    }
}
