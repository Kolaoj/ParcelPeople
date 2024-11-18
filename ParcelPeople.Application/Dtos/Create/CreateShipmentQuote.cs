namespace ParcelPeople.Application.Dtos.Add
{
    public class CreateShipmentQuote
    {
        public required Guid SenderId { get; set; }
        public required string ReceiverName { get; set; }
        public required IEnumerable<CreateShipmentCity> Cities { get; set; }
        public required IEnumerable<CreateParcel> Parcels { get; set; }
    }
}
