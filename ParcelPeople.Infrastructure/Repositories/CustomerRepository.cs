using Microsoft.EntityFrameworkCore;
using ParcelPeople.Domain.Models;
using ParcelPeople.Infrastructure.DbContexts;
using ParcelPeople.Infrastructure.Repositories.Interfaces;

namespace ParcelPeople.Infrastructure.Repositories
{
    public class CustomerRepository(ShipmentDbContext context) : ICustomerRepository
    {
        private readonly ShipmentDbContext context = context ?? throw new ArgumentNullException(nameof(context));

        public async Task<Customer> Add(Customer customer)
        {
            ArgumentNullException.ThrowIfNull(customer);

            await context.Customers.AddAsync(customer);
            await context.SaveChangesAsync();

            return customer;
        }

        public async Task<Customer?> FindCustomer(string? email, string? contactNumber)
        {
            return await context.Customers
            .Include(c => c.Shipments)
                .ThenInclude(s => s.Cities)
                .Include(c => c.Shipments)
                .ThenInclude(s => s.Parcels)
            .FirstOrDefaultAsync(c =>
             (email != null && c.Email == email) ||
             (contactNumber != null && c.ContactNumber == contactNumber));
        }

        public async Task<Customer> GetCustomerById(Guid id)
        {
            return await context.Customers
                .Include(c => c.Shipments)
                .ThenInclude(s => s.Cities)
                .Include(c => c.Shipments)
                .ThenInclude(s => s.Parcels)
                .FirstOrDefaultAsync(c => c.Id == id) ?? throw new Exception("Customer could not be found");
        }
    }
}
