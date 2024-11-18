using ParcelPeople.Application.Dtos.Create;
using ParcelPeople.Application.Dtos.Read;
using ParcelPeople.Domain.Models;

namespace ParcelPeople.Application.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<Customer> CreateCustomer(CreateCustomer createCustomer);
        Task<Customer?> FindCustomer(CustomerSearch shipmentSearch);
        Task<Customer> GetCustomerById(Guid id);
    }
}
