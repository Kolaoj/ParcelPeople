using ParcelPeople.Application.Dtos.Add;
using ParcelPeople.Domain.Enums;
using Swashbuckle.AspNetCore.Filters;

public class ShippingExampleRequests : IExamplesProvider<CreateShipmentQuote>
{
    public CreateShipmentQuote GetExamples()
    {
        return new CreateShipmentQuote
        {
            SenderId = Guid.Parse("C2E9E363-2E20-433A-88D6-3C1090D85D52"),
            ReceiverName = "Bob Bobbington",
            Cities = new List<CreateShipmentCity>
            {
                new CreateShipmentCity
                {
                    Origin = true,
                    Destination = false,
                    CityId = 2
                },
                new CreateShipmentCity
                {
                    Origin = false,
                    Destination = true,
                    CityId = 1
                }
            },
            Parcels = new List<CreateParcel>
            {
                new CreateParcel
                {
                    Type = ParcelTypes.Envelope,
                    Dimensions = 10
                },
                new CreateParcel
                {
                    Type = ParcelTypes.Package,
                    Dimensions = 50
                }
            }
        };
    }
}

