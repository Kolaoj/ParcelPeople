using Microsoft.AspNetCore.Mvc;
using ParcelPeople.Application.Dtos.Create;
using ParcelPeople.Application.Dtos.Read;
using ParcelPeople.Application.Services.Interfaces;
using ParcelPeople.Domain.Exceptions;
using Swashbuckle.AspNetCore.Annotations;

namespace ParcelPeople.Api.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class Customers(ICustomerService customerService) : ControllerBase
    {
        private readonly ICustomerService customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Gets a customer")]
        public async Task<IResult> GetCustomer(Guid id)
        {
            try
            {
                var customer = await customerService.GetCustomerById(id);

                if (customer == null)
                {
                    return Results.Problem(title: "Customer does not exist", detail: $"A customer with the id {id} does not exist", statusCode: StatusCodes.Status404NotFound);
                }

                return Results.Ok(customer);
            }
            catch (Exception ex)
            {
                return Results.Problem(detail: $"{ex.Message}", statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("search")]
        [SwaggerOperation(Summary = "Searches for a customer", Description = "Searches for a customer by email or phone")]
        public async Task<IResult> FindCustomer([FromBody] CustomerSearch customerSearch)
        {
            try
            {
                var customer = await customerService.FindCustomer(customerSearch);

                if (customer == null)
                {
                    return Results.Problem(title: "Customer could not be found", detail: $"The search properties used could not find the customer", statusCode: StatusCodes.Status404NotFound);
                }

                return Results.Ok(customer);
            }
            catch (Exception ex)
            {
                return Results.Problem(detail: $"{ex.Message}", statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Creates a customer", Description = "Creates a new customer. NB: Email and phone must be unique")]
        public async Task<IResult> CreateCustomer([FromBody] CreateCustomer createCustomer)
        {
            try
            {
                var customer = await customerService.CreateCustomer(createCustomer);

                var url = Url.Link("GetCustomer", new { id = customer.Id });

                return Results.Created(url, customer);
            }
            catch(CustomerAlreadyExistsException ex)
            {
                return Results.Problem(detail: $"{ex.Message}", statusCode: StatusCodes.Status409Conflict);
            }
            catch (Exception ex)
            {
                return Results.Problem(detail: $"{ex.Message}", statusCode: StatusCodes.Status500InternalServerError);
            }
        }
    }
}
