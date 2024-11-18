using Microsoft.EntityFrameworkCore;
using ParcelPeople.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace ParcelPeople.Domain.Models
{
    [Index(nameof(Email), nameof(ContactNumber), IsUnique = true)]
    public class Customer
    {
        public Guid Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        [MaxLength(100)]
        public required string Email { get; set; }
        [MaxLength(16)]
        public required string ContactNumber { get; set; }
        public ICollection<Shipment> Shipments { get; set; } = [];
    }
}
