using Microsoft.AspNetCore.Mvc;
using ParcelPeople.Application.Dtos.Add;
using ParcelPeople.Application.Dtos.Update;
using ParcelPeople.Application.Services.Interfaces;
using ParcelPeople.Domain.Enums;
using ParcelPeople.Domain.Exceptions;
using Swashbuckle.AspNetCore.Annotations;

namespace ParcelPeople.Api.Controllers
{
    [ApiController]
    [Route("api/shipments")]
    public class Shipments(IShipmentService shipmentService) : ControllerBase
    {
        private readonly IShipmentService shipmentService = shipmentService ?? throw new ArgumentNullException(nameof(shipmentService));

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Gets a shipment")]
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
        [SwaggerOperation(Summary = "Creates a new shipment quote")]
        public async Task<IResult> CreateShipmentQuote([FromBody] CreateShipmentQuote createShipmentQuote)
        {
            try
            {
                var quote = await shipmentService.CreateQuote(createShipmentQuote);

                var uri = Url.Link("GetShipment", new { id = quote.Id });

                return Results.Created(uri, quote);
            }
            catch (CityDoesNotExistExcepion ex)
            {
                return Results.Problem(detail: $"{ex.Message}", statusCode: StatusCodes.Status400BadRequest);
            }
            catch(OriginDestinationConflictException ex)
            {
                return Results.Problem(detail: $"{ex.Message}", statusCode: StatusCodes.Status400BadRequest);
            }
            catch (Exception ex)
            {
                return Results.Problem(detail: $"{ex.Message}", statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPatch("{id}/create")]
        [SwaggerOperation(Summary = "Updates a shipment quote to a real shipment", Description = "The status of a shipment will go from quote to pending")]
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
        [SwaggerOperation(Summary = "Updates a shipment status")]
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
        [SwaggerOperation(Summary = "Updates a shipment city's status", Description = "Moves a shipment by updating it's status and potentially time of arrival")]
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
