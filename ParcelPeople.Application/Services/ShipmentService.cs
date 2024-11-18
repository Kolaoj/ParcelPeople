using ParcelPeople.Application.Dtos.Add;
using ParcelPeople.Application.Dtos.Create.Mappings;
using ParcelPeople.Application.Dtos.Update;
using ParcelPeople.Application.Dtos.Update.Mappings;
using ParcelPeople.Application.Services.Interfaces;
using ParcelPeople.Domain.Entities;
using ParcelPeople.Domain.Enums;
using ParcelPeople.Domain.Exceptions;
using ParcelPeople.Infrastructure.Repositories.Interfaces;

namespace ParcelPeople.Application.Services
{
    public class ShipmentService(IShipmentRepository shipmentRepository, IParcelService parcelService, ICityService cityService) : IShipmentService
    {
        private readonly IShipmentRepository shipmentRepository = shipmentRepository ?? throw new ArgumentNullException(nameof(shipmentRepository));
        private readonly IParcelService parcelService = parcelService ?? throw new ArgumentNullException(nameof(parcelService));
        private readonly ICityService cityService = cityService ?? throw new ArgumentNullException(nameof(cityService));

        public async Task<Shipment> CreateQuote(CreateShipmentQuote createQuote)
        {
            var cities = await cityService.GetCitiesByIds(createQuote.Cities.Select(c => c.CityId));

            ValidateShipmentCities(createQuote.Cities, cities);
            
            var shipmentCostAndCultureCode = await CalculateShipmentCost(createQuote.Cities, cities, createQuote.Parcels);

            var shipment = createQuote.ToQuote(cities, shipmentCostAndCultureCode.Item1, shipmentCostAndCultureCode.Item2);

            shipment = await shipmentRepository.Add(shipment);

            return shipment;
        }

        public async Task CreateShipment(Guid shipmentId)
        {
            var shipment = await shipmentRepository.GetById(shipmentId);

            shipment.Status = ShipmentStatus.Pending;

            await shipmentRepository.Update(shipment);
        }

        public async Task<Shipment> GetShipment(Guid shipmentId) => await shipmentRepository.GetById(shipmentId);

        public async Task MoveShipment(Guid shipmentCityId, MoveShipmentCity moveShipmentCity)
        {
            var shipmentCity = await shipmentRepository.GetShipmentCityById(shipmentCityId);

            shipmentCity.UpdateShipmentCity(moveShipmentCity);

            await shipmentRepository.UpdateShipmentCity(shipmentCity);
        }

        public async Task UpdateShipmentStatus(Guid shipmentId, ShipmentStatus shipmentStatus)
        {
            var shipment = await shipmentRepository.GetById(shipmentId);

            shipment.Status = shipmentStatus;

            await shipmentRepository.Update(shipment);
        }


        private void ValidateShipmentCities(IEnumerable<CreateShipmentCity> shipmentCities, IEnumerable<City> cities) 
        {
            var unknownCity = shipmentCities.FirstOrDefault(sc => !cities.Select(c => c.Id).Contains(sc.CityId));

            if (unknownCity != null)
            {
                throw new CityDoesNotExistExcepion($"The city with id: {unknownCity.CityId} does not exist");
            }

            if (shipmentCities.Count() <= 1)
            {
                throw new OriginDestinationConflictException("There must be at least two shipping cities.");
            }
            if (shipmentCities.Any(c => c.Origin && c.Destination))
            {
                throw new OriginDestinationConflictException("One city can not be both an origin and a destination");
            }
            if (!shipmentCities.Any(c => c.Origin) || !shipmentCities.Any(c => c.Destination))
            {
                throw new OriginDestinationConflictException("There must be at least 1 city of origin and 1 destination city");
            }
        }

        private async Task<Tuple<decimal, string>> CalculateShipmentCost(IEnumerable<CreateShipmentCity> createShipmentCities, IEnumerable<City> cities, IEnumerable<CreateParcel> createParcels)
        {
            decimal cost = 0M;
            var totalCities = createShipmentCities.Count();

            var originShipmentCity = createShipmentCities.FirstOrDefault(c => c.Origin) ?? throw new Exception("The shipment does not contain an origin city");

            var originCity = cities.FirstOrDefault(c => c.Id == originShipmentCity.CityId) ?? throw new Exception("This city does not exist");

            var originCostPerCity = originCity.OriginCost;

            var packages = createParcels.Where(p => p.Type == ParcelTypes.Package);
            var envelopes = createParcels.Where(p => p.Type == ParcelTypes.Envelope);

            if (packages.Any())
            {
                var packageDimensions = packages.Select(p => p.Dimensions ?? throw new Exception("At least one package does not contain a dimension"));
                var totalSurcharge = await parcelService.GetTotalSurcharge(packageDimensions.Select(pd => pd)) + 1;

                var packageCostPerCity = originCostPerCity * totalSurcharge;
                cost += packageCostPerCity * totalCities * packages.Count();
            }
            if (envelopes.Any())
            {
                cost += originCostPerCity * totalCities * envelopes.Count();
            }

            return new Tuple<decimal, string>(cost, originCity.CultureCode);
        }
    }
}
