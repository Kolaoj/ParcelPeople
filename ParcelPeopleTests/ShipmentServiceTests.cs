using Moq;
using ParcelPeople.Application.Dtos.Add;
using ParcelPeople.Application.Services;
using ParcelPeople.Application.Services.Interfaces;
using ParcelPeople.Domain.Entities;
using ParcelPeople.Domain.Enums;
using ParcelPeople.Infrastructure.Repositories.Interfaces;

namespace ParcelPeopleTests
{
    public partial class ShipmentServiceTests
    {
        private readonly Mock<IShipmentRepository> shipmentRepositoryMock;
        private readonly Mock<IParcelService> parcelServiceMock;
        private readonly Mock<ICityService> cityServiceMock;
        private readonly ShipmentService sut;

        public ShipmentServiceTests()
        {
            shipmentRepositoryMock = new Mock<IShipmentRepository>();
            parcelServiceMock = new Mock<IParcelService>();
            cityServiceMock = new Mock<ICityService>();
            sut = new ShipmentService(
                shipmentRepositoryMock.Object,
                parcelServiceMock.Object,
                cityServiceMock.Object
            );
        }

       
    }
}