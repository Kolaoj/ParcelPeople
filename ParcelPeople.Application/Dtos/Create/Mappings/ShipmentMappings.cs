using ParcelPeople.Application.Dtos.Add;
using ParcelPeople.Domain.Entities;
using ParcelPeople.Domain.Enums;
using System.Globalization;

namespace ParcelPeople.Application.Dtos.Create.Mappings
{
    public static class ShipmentMappings
    {
        public static Shipment ToQuote(this CreateShipmentQuote createQuote, IEnumerable<City> cities, decimal cost, string cultureCodw)
        {
            return new Shipment
            {
                CustomerId = createQuote.SenderId,
                ReceiverName = createQuote.ReceiverName,
                Status = ShipmentStatus.Quote,
                Cities = createQuote.Cities.Select(c => new ShipmentCity
                {
                    Origin = c.Origin,
                    Destination = c.Destination,
                    Status = ShipmentCityStatus.Awaiting,
                    CityId = cities.First(city => city.Id == c.CityId).Id
                }).ToList(),
                Parcels = createQuote.Parcels.Select(p => new Parcel
                {
                    Type = p.Type,
                    Dimensions = p.Dimensions
                }).ToList(),
                Cost = cost,
                CostDisplayed = cost.ToString("C", new CultureInfo(cultureCodw))
            };
        }
    }
}
