using Moq;
using ParcelPeople.Application.Services;
using ParcelPeople.Application.Services.Interfaces;
using ParcelPeople.Infrastructure.Repositories.Interfaces;

namespace ParcelPeople.Application.Tests
{
    public class ShipmentServiceTests
    {
        private readonly Mock<IShipmentRepository> _shipmentRepositoryMock;
        private readonly Mock<IParcelService> _parcelServiceMock;
        private readonly Mock<ICityService> _cityServiceMock;
        private readonly ShipmentService _shipmentService;

        public ShipmentServiceTests()
        {
            _shipmentRepositoryMock = new Mock<IShipmentRepository>();
            _parcelServiceMock = new Mock<IParcelService>();
            _cityServiceMock = new Mock<ICityService>();
            _shipmentService = new ShipmentService(
                _shipmentRepositoryMock.Object,
                _parcelServiceMock.Object,
                _cityServiceMock.Object
            );
        }
    }

}