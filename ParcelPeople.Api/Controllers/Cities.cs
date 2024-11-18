using Microsoft.AspNetCore.Mvc;
using ParcelPeople.Application.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ParcelPeople.Api.Controllers
{
    [ApiController]
    [Route("api/cities")]
    public class Cities(ICityService cityService) : ControllerBase
    {
        private readonly ICityService cityService = cityService ?? throw new ArgumentNullException(nameof(cityService));

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Gets a city")]
        public async Task<IResult> GetCity(int id)
        {
            try
            {
                var city = await cityService.GetCityById(id);

                if (city == null)
                {
                    return Results.Problem(title: "City does not exist", detail: $"A city with the id {id} does not exist", statusCode: StatusCodes.Status404NotFound);
                }

                return Results.Ok(city);
            }
            catch (Exception ex)
            {
                return Results.Problem(detail: $"{ex.Message}", statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Gets all cities")]
        public async Task<IResult> GetAllCities([FromQuery] int top = 10, [FromQuery] int skip = 0)
        {
            try
            {
                var cities = await cityService.GetAllCities(top, skip);

                return Results.Ok(cities);
            }
            catch (Exception ex)
            {
                return Results.Problem(detail: $"{ex.Message}", statusCode: StatusCodes.Status500InternalServerError);
            }
        }
    }
}
