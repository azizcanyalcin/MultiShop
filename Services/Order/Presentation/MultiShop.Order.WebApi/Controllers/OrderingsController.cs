using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Order.Application.Features.Mediator.Commands.OrderingCommands;
using MultiShop.Order.Application.Features.Mediator.Queries.OrderingQueries;

namespace MultiShop.Order.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderingsController : ControllerBase
    {
        private readonly IMediator mediator;

        public OrderingsController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllOrderings()
        {
            var values = await mediator.Send(new GetOrderingQuery());

            return Ok(values);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderingById(int id)
        {
            var values = await mediator.Send(new GetOrderingByIdQuery(id));

            return Ok(values);
        }


        [HttpPost]
        public async Task<IActionResult> CreateOrdering(CreateOrderingCommand command)
        {
            await mediator.Send(command);
            return Ok("Ordering created successfully!");
        }


        [HttpPut]
        public async Task<IActionResult> UpdateOrdering(UpdateOrderingCommand command)
        {
            await mediator.Send(command);
            return Ok("Ordering updated successfully!");
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrdering(int id)
        {
            await mediator.Send(new DeleteOrderingCommand(id));
            return Ok("Ordering deleted successfully!");
        }
    }
}
