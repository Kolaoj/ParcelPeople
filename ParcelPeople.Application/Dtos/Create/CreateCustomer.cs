namespace ParcelPeople.Application.Dtos.Create
{
    public class CreateCustomer
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string ContactNumber { get; set; }
    }
}
