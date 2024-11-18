using ParcelPeople.Application.Dtos.Create;
using ParcelPeople.Application.Dtos.Create.Mappings;
using ParcelPeople.Application.Dtos.Read;
using ParcelPeople.Application.Services.Interfaces;
using ParcelPeople.Domain.Models;
using ParcelPeople.Infrastructure.Repositories.Interfaces;

namespace ParcelPeople.Application.Services
{
    public class CustomerService(ICustomerRepository customerRepository) : ICustomerService
    {
        private readonly ICustomerRepository customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));

        public async Task<Customer?> FindCustomer(CustomerSearch shipmentSearch) => await customerRepository.FindCustomer(shipmentSearch.Email, shipmentSearch.ContactNumber);
        public async Task<Customer> GetCustomerById(Guid id) => await customerRepository.GetCustomerById(id);

        public async Task<Customer> CreateCustomer(CreateCustomer createCustomer) => await customerRepository.Add(createCustomer.ToModel());
    }
}
