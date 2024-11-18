using ParcelPeople.Domain.Models;

namespace ParcelPeople.Application.Dtos.Create.Mappings
{
    public static class CustomerMappings
    {
        public static Customer ToModel(this CreateCustomer createCustomer)
        {
            return new Customer
            {
                FirstName = createCustomer.FirstName,
                LastName = createCustomer.LastName,
                Email = createCustomer.Email,
                ContactNumber = createCustomer.ContactNumber
            };
        }
    }
}
