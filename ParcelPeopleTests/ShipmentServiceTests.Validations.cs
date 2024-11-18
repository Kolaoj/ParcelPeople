using Moq;
using ParcelPeople.Application.Dtos.Add;
using ParcelPeople.Domain.Entities;
using ParcelPeople.Domain.Exceptions;

namespace ParcelPeopleTests
{
    public partial class ShipmentServiceTests
    {
        [Fact]
        public async Task CreateQuoteThrowsExceptionWhenUnknownCityExists()
        {
            // Arrange
            var createQuote = new CreateShipmentQuote
            {
                SenderId = Guid.NewGuid(),
                ReceiverName = "John Doe",
                Cities = new List<CreateShipmentCity>
            {
                new CreateShipmentCity { CityId = 1, Origin = true, Destination = false },
                new CreateShipmentCity { CityId = 2, Origin = false, Destination = true },
            },
                Parcels = new List<CreateParcel>()
            };

            cityServiceMock
                .Setup(service => service.GetCitiesByIds(It.IsAny<IEnumerable<int>>()))
                .ReturnsAsync(new List<City>
                {
                new City { Id = 1, Name = "City1", CultureCode = "en-GB-GB", Country = "United Kingdom", OriginCost = 10M }
                });

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CityDoesNotExistExcepion>(() => sut.CreateQuote(createQuote));
            Assert.Equal("The city with id: 2 does not exist", exception.Message);
        }

        [Fact]
        public async Task CreateQuoteThrowsExceptionWhenLessThanTwoCities()
        {
            // Arrange
            var createQuote = new CreateShipmentQuote
            {
                SenderId = Guid.NewGuid(),
                ReceiverName = "John Doe",
                Cities = new List<CreateShipmentCity>
            {
                new CreateShipmentCity { CityId = 1, Origin = true, Destination = false },
            },
                Parcels = new List<CreateParcel>()
            };

            cityServiceMock
                .Setup(service => service.GetCitiesByIds(It.IsAny<IEnumerable<int>>()))
                .ReturnsAsync(new List<City>
                {
                new City { Id = 1, Name = "City1", CultureCode = "en-GB", Country = "United Kingdom", OriginCost = 10M }
                });

            // Act & Assert
            var exception = await Assert.ThrowsAsync<OriginDestinationConflictException>(() => sut.CreateQuote(createQuote));
            Assert.Equal("There must be at least two shipping cities.", exception.Message);
        }

        [Fact]
        public async Task CreateQuoteThrowsExceptionWhenCityIsBothOriginAndDestination()
        {
            // Arrange
            var createQuote = new CreateShipmentQuote
            {
                SenderId = Guid.NewGuid(),
                ReceiverName = "John Doe",
                Cities = new List<CreateShipmentCity>
            {
                new CreateShipmentCity { CityId = 1, Origin = true, Destination = true },
                new CreateShipmentCity { CityId = 2, Origin = false, Destination = true },
            },
                Parcels = new List<CreateParcel>()
            };

            cityServiceMock
                .Setup(service => service.GetCitiesByIds(It.IsAny<IEnumerable<int>>()))
                .ReturnsAsync(new List<City>
                {
                new City { Id = 1, Name = "City1", CultureCode = "en-GB", Country = "United Kingdom", OriginCost = 10M },
                new City { Id = 2, Name = "City2", CultureCode = "fr", Country = "France", OriginCost = 15M }
                });

            // Act & Assert
            var exception = await Assert.ThrowsAsync<OriginDestinationConflictException>(() => sut.CreateQuote(createQuote));
            Assert.Equal("One city can not be both an origin and a destination", exception.Message);
        }

        [Fact]
        public async Task CreateQuoteThrowsExceptionWhenNoOriginCityExists()
        {
            // Arrange
            var createQuote = new CreateShipmentQuote
            {
                SenderId = Guid.NewGuid(),
                ReceiverName = "John Doe",
                Cities = new List<CreateShipmentCity>
            {
                new CreateShipmentCity { CityId = 1, Origin = false, Destination = true },
                new CreateShipmentCity { CityId = 2, Origin = false, Destination = true },
            },
                Parcels = new List<CreateParcel>()
            };

            cityServiceMock
                .Setup(service => service.GetCitiesByIds(It.IsAny<IEnumerable<int>>()))
                .ReturnsAsync(new List<City>
                {
                new City { Id = 1, Name = "City1", CultureCode = "en-GB", Country = "United Kingdom", OriginCost = 10M },
                new City { Id = 2, Name = "City2", CultureCode = "fr", Country = "France", OriginCost = 15M }
                });

            // Act & Assert
            var exception = await Assert.ThrowsAsync<OriginDestinationConflictException>(() => sut.CreateQuote(createQuote));
            Assert.Equal("There must be at least 1 city of origin and 1 destination city", exception.Message);
        }

        [Fact]
        public async Task CreateQuoteThrowsExceptionWhenNoDestinationCityExists()
        {
            // Arrange
            var createQuote = new CreateShipmentQuote
            {
                SenderId = Guid.NewGuid(),
                ReceiverName = "John Doe",
                Cities = new List<CreateShipmentCity>
            {
                new CreateShipmentCity { CityId = 1, Origin = true, Destination = false },
                new CreateShipmentCity { CityId = 2, Origin = true, Destination = false },
            },
                Parcels = new List<CreateParcel>()
            };

            cityServiceMock
                .Setup(service => service.GetCitiesByIds(It.IsAny<IEnumerable<int>>()))
                .ReturnsAsync(new List<City>
                {
                new City { Id = 1, Name = "City1", CultureCode = "en-GB", Country = "United Kingdom", OriginCost = 10M },
                new City { Id = 2, Name = "City2", CultureCode = "fr", Country = "France", OriginCost = 15M }
                });

            // Act & Assert
            var exception = await Assert.ThrowsAsync<OriginDestinationConflictException>(() => sut.CreateQuote(createQuote));
            Assert.Equal("There must be at least 1 city of origin and 1 destination city", exception.Message);
        }

        [Fact]
        public async Task CreateQuotePassesWhenValidCitiesProvided()
        {
            // Arrange
            var createQuote = new CreateShipmentQuote
            {
                SenderId = Guid.NewGuid(),
                ReceiverName = "John Doe",
                Cities = new List<CreateShipmentCity>
            {
                new CreateShipmentCity { CityId = 1, Origin = true, Destination = false },
                new CreateShipmentCity { CityId = 2, Origin = false, Destination = true },
            },
                Parcels = new List<CreateParcel>()
            };

            cityServiceMock
                .Setup(service => service.GetCitiesByIds(It.IsAny<IEnumerable<int>>()))
                .ReturnsAsync(new List<City>
                {
                new City { Id = 1, Name = "City1", CultureCode = "en-GB", Country = "United Kingdom", OriginCost = 10M },
                new City { Id = 2, Name = "City2", CultureCode = "fr", Country = "France", OriginCost = 15M }
                });

            shipmentRepositoryMock
                .Setup(repo => repo.Add(It.IsAny<Shipment>()))
                .ReturnsAsync(It.IsAny<Shipment>());

            // Act
            var exception = await Record.ExceptionAsync(() => sut.CreateQuote(createQuote));

            Assert.Null(exception);
        }
    }
}
