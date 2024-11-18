using Moq;
using ParcelPeople.Application.Dtos.Add;
using ParcelPeople.Domain.Entities;
using ParcelPeople.Domain.Enums;

namespace ParcelPeopleTests
{
    public partial class ShipmentServiceTests
    {
        [Theory]
        [InlineData(60, 0.2, 24.0, "£24.00")]
        [InlineData(40, 0.0, 20.0, "£20.00")]
        public async Task CreateQuoteShouldCalculateCostForPackageWhenStartingFromUK(double dimensions, decimal surcharge, decimal expectedCost, string expectedCostDisplayed)
        {
            // Arrange
            var createQuote = new CreateShipmentQuote
            {
                SenderId = Guid.NewGuid(),
                ReceiverName = "Bob Ascot",
                Cities = new List<CreateShipmentCity>
                {
                    new CreateShipmentCity { CityId = 1, Origin = true, Destination = false },
                    new CreateShipmentCity { CityId = 2, Origin = false, Destination = true }
                },
                Parcels = new List<CreateParcel>
                {
                    new CreateParcel { Type = ParcelTypes.Package, Dimensions = dimensions }
                }
            };

            var cities = new List<City>
            {
                new City { Id = 1, Name = "London", Country = "UK", OriginCost = 10, CultureCode = "en-GB" },
                new City { Id = 2, Name = "Dublin", Country = "Ireland", OriginCost = 12, CultureCode = "en-IE" }
            };

            cityServiceMock.Setup(x => x.GetCitiesByIds(It.IsAny<IEnumerable<int>>()))
                .ReturnsAsync(cities);

            parcelServiceMock.Setup(x => x.GetTotalSurcharge(It.IsAny<IEnumerable<double>>()))
                .ReturnsAsync(surcharge);

            shipmentRepositoryMock.Setup(x => x.Add(It.IsAny<Shipment>()))
                .ReturnsAsync((Shipment shipment) => shipment);

            // Act
            var result = await sut.CreateQuote(createQuote);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedCost, result.Cost);
            Assert.Equal(expectedCostDisplayed, result.CostDisplayed);
            shipmentRepositoryMock.Verify(x => x.Add(It.IsAny<Shipment>()), Times.Once);
        }

        [Theory]
        [InlineData(60, 0.2, 28.8, "€28.80")]
        [InlineData(40, 0.0, 24.0, "€24.00")]
        public async Task CreateQuoteShouldCalculateCostForPackageWhenStartingFromEu(double dimensions, decimal surcharge, decimal expectedCost, string expectedCostDisplayed)
        {
            // Arrange
            var createQuote = new CreateShipmentQuote
            {
                SenderId = Guid.NewGuid(),
                ReceiverName = "Bob Ascot",
                Cities = new List<CreateShipmentCity>
                {
                    new CreateShipmentCity { CityId = 1, Origin = false, Destination = true },
                    new CreateShipmentCity { CityId = 2, Origin = true, Destination = false }
                },
                Parcels = new List<CreateParcel>
                {
                    new CreateParcel { Type = ParcelTypes.Package, Dimensions = dimensions }
                }
            };

            var cities = new List<City>
            {
                new City { Id = 1, Name = "London", Country = "UK", OriginCost = 10, CultureCode = "en-GB" },
                new City { Id = 2, Name = "Dublin", Country = "Ireland", OriginCost = 12, CultureCode = "en-IE" }
            };

            cityServiceMock.Setup(x => x.GetCitiesByIds(It.IsAny<IEnumerable<int>>()))
                .ReturnsAsync(cities);

            parcelServiceMock.Setup(x => x.GetTotalSurcharge(It.IsAny<IEnumerable<double>>()))
                .ReturnsAsync(surcharge);

            shipmentRepositoryMock.Setup(x => x.Add(It.IsAny<Shipment>()))
                .ReturnsAsync((Shipment shipment) => shipment);

            // Act
            var result = await sut.CreateQuote(createQuote);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedCost, result.Cost);
            Assert.Equal(expectedCostDisplayed, result.CostDisplayed);
            shipmentRepositoryMock.Verify(x => x.Add(It.IsAny<Shipment>()), Times.Once);
        }

        [Theory]
        [InlineData(ParcelTypes.Package, 60, 0.2, 24.0, "£24.00")]  // Package with surcharge
        [InlineData(ParcelTypes.Envelope, 60, 0.0, 20.0, "£20.00")]  // Envelope, no surcharge
        public async Task CreateQuoteShouldCalculateCostForParcelType_AndIgnoreEnvelopeSize(ParcelTypes parcelType, double dimensions, decimal surcharge, decimal expectedCost, string expectedCostDisplayed)
        {
            // Arrange
            var createQuote = new CreateShipmentQuote
            {
                SenderId = Guid.NewGuid(),
                ReceiverName = "Bob Ascot",
                Cities = new List<CreateShipmentCity>
                {
                    new CreateShipmentCity { CityId = 1, Origin = true, Destination = false },
                    new CreateShipmentCity { CityId = 2, Origin = false, Destination = true }
                },
                Parcels = new List<CreateParcel>
                {
                    new CreateParcel { Type = parcelType, Dimensions = dimensions }
                }
            };

            var cities = new List<City>
            {
                new City { Id = 1, Name = "London", Country = "UK", OriginCost = 10, CultureCode = "en-GB" },
                new City { Id = 2, Name = "Dublin", Country = "Ireland", OriginCost = 12, CultureCode = "en-IE" }
            };

            cityServiceMock.Setup(x => x.GetCitiesByIds(It.IsAny<IEnumerable<int>>()))
                .ReturnsAsync(cities);

            parcelServiceMock.Setup(x => x.GetTotalSurcharge(It.IsAny<IEnumerable<double>>()))
                .ReturnsAsync(surcharge);

            shipmentRepositoryMock.Setup(x => x.Add(It.IsAny<Shipment>()))
                .ReturnsAsync((Shipment shipment) => shipment);

            // Act
            var result = await sut.CreateQuote(createQuote);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedCost, result.Cost);
            Assert.Equal(expectedCostDisplayed, result.CostDisplayed);
            shipmentRepositoryMock.Verify(x => x.Add(It.IsAny<Shipment>()), Times.Once);
        }
    }
}
