using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Application.Orders.Commands;
using OrderManagement.Application.Orders.Queries;
using OrderManagement.Application.Specifications;

namespace OrderManagement.Api.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
        {
            var orderId = await _mediator.Send(command);
            return Ok(new { OrderId = orderId });
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetOrder(Guid id)
        {
            var query = new OrderByIdQuery{ Id=id };
            var orderDetail = await _mediator.Send(query);
            return Ok(new { OrderDetail = orderDetail });
        }

        [HttpGet("{page:int}/{size:int}")]
        public async Task<IActionResult> GetListOrder(int page, int size)
        {
            var query = new InCompleteOrderQuery{ Page=page, Size=size };
            var orderDetail = await _mediator.Send(query);
            return Ok(new
            {
                OrderList = orderDetail.Item1,
                TotalCount = orderDetail.Item2,
            });
        }
    }
}
