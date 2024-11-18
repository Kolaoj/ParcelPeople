using Microsoft.AspNetCore.Mvc;
using ParcelPeople.Application.Dtos.Add;
using ParcelPeople.Application.Dtos.Update;
using ParcelPeople.Application.Services.Interfaces;
using ParcelPeople.Domain.Enums;

namespace ParcelPeople.Api.Controllers
{
    [ApiController]
    [Route("api/shipments")]
    public class Shipments(IShipmentService shipmentService) : ControllerBase
    {
        private readonly IShipmentService shipmentService = shipmentService ?? throw new ArgumentNullException(nameof(shipmentService));

        [HttpGet("{id}")]
        public async Task<IResult> GetShipment(Guid id)
        {
            try
            {
                var shipment = await shipmentService.GetShipment(id);

                if (shipment == null)
                {
                    return Results.Problem(title: "Shipment does not exist", detail: $"A shipment with the id {id} does not exist", statusCode: StatusCodes.Status404NotFound);
                }

                return Results.Ok(shipment);
            }
            catch (Exception ex)
            {
                return Results.Problem(detail: $"{ex.Message}", statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("quote")]
        public async Task<IResult> CreateShipmentQuote([FromBody] CreateShipmentQuote createShipmentQuote)
        {
            try
            {
                var quote = await shipmentService.CreateQuote(createShipmentQuote);

                var uri = Url.Link("GetShipment", new { id = quote.Id });

                return Results.Created(uri, quote);
            }
            catch (Exception ex)
            {
                return Results.Problem(detail: $"{ex.Message}", statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPatch("{id}/create")]
        public async Task<IResult> CreateShipment(Guid id)
        {
            try
            {
                await shipmentService.CreateShipment(id);

                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.Problem(detail: $"{ex.Message}", statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPatch("{id}/stauses/{status}update")]
        public async Task<IResult> UpdateShipmentStatus(Guid id, ShipmentStatus status)
        {
            try
            {
                await shipmentService.UpdateShipmentStatus(id, status);

                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.Problem(detail: $"{ex.Message}", statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPatch("shipmentCities/{id}/update")]
        public async Task<IResult> MoveShipmentCity(Guid id, [FromBody] MoveShipmentCity moveShipmentCity)
        {
            try
            {
                await shipmentService.MoveShipment(id, moveShipmentCity);

                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.Problem(detail: $"{ex.Message}", statusCode: StatusCodes.Status500InternalServerError);
            }
        }
    }
}
