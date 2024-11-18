using ParcelPeople.Domain.Models;

namespace ParcelPeople.Infrastructure.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer> Add(Customer customer);
        Task<Customer?> FindCustomer(string? email, string? contactNumber);
        Task<Customer> GetCustomerById(Guid id);
    }
}
