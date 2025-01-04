using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Order.Application.Features.CQRS.Commands.OrderDetailCommands;
using MultiShop.Order.Application.Features.CQRS.Handlers.OrderDetailHandlers;
using MultiShop.Order.Application.Features.CQRS.Queries.OrderDetailQueries;

namespace MultiShop.Order.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly GetOrderDetailQueryHandler getOrderDetailQueryHandler;
        private readonly GetOrderDetailByIdQueryHandler getOrderDetailByIdQueryHandler;
        private readonly CreateOrderDetailCommandHandler createOrderDetailCommandHandler;
        private readonly UpdateOrderDetailCommandHandler updateOrderDetailCommandHandler;
        private readonly DeleteOrderDetailCommandHandler deleteOrderDetailCommandHandler;

        public OrderDetailsController(GetOrderDetailQueryHandler getOrderDetailQueryHandler, GetOrderDetailByIdQueryHandler getOrderDetailByIdQueryHandler, CreateOrderDetailCommandHandler createOrderDetailCommandHandler, DeleteOrderDetailCommandHandler deleteOrderDetailCommandHandler, UpdateOrderDetailCommandHandler updateOrderDetailCommandHandler)
        {
            this.getOrderDetailQueryHandler = getOrderDetailQueryHandler;
            this.getOrderDetailByIdQueryHandler = getOrderDetailByIdQueryHandler;
            this.createOrderDetailCommandHandler = createOrderDetailCommandHandler;
            this.deleteOrderDetailCommandHandler = deleteOrderDetailCommandHandler;
            this.updateOrderDetailCommandHandler = updateOrderDetailCommandHandler;
        }

        [HttpGet]
        public async Task <IActionResult> GetAllOrderDetail()
        {
            var values = await getOrderDetailQueryHandler.Handle();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderDetailById(int id)
        {
            var values = await getOrderDetailByIdQueryHandler.Handle(new GetOrderDetailByIdQuery(id));
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderDetail(CreateOrderDetailCommand command)
        {

            await createOrderDetailCommandHandler.Handle(command);

            return Ok("Order Detail added succesfully!");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrderDetail(UpdateOrderDetailCommand command)
        {
            await updateOrderDetailCommandHandler.Handle(command);

            return Ok("Order Detail updated succesfully!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderDetail(int id)
        {
            await deleteOrderDetailCommandHandler.Handle(new DeleteOrderDetailCommand(id));

            return Ok("Order Detail deleted succesfully!");
        }
    }
}
