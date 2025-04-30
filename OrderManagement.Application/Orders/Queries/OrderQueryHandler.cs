using GbLib.BuildingBlock.Application.CQRS;
using OrderManagement.Application.Orders.Dtos;
using OrderManagement.Domain.Interfaces;

namespace OrderManagement.Application.Orders.Queries
{
    public class OrderQueryHandler : QueryHandler<OrderByIdQuery, OrderDto>
    {
        private readonly IOrderQueryRepository _orderRepository;
        public OrderQueryHandler(IOrderQueryRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public override async Task<OrderDto> Handle(OrderByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _orderRepository.GetByIdAsync(request.Id);
            return new OrderDto
            {
                CustomerAddress = result?.CustomerAddress??"",
                CustomerEmail = result?.CustomerEmail??"",
                CustomerName = result?.CustomerName??"",
                CustomerPhone = result?.CustomerPhone??"",
                OrderItems = result?.OrderItems.Select(oi => new OrderItemDto
                {
                    ProductName = oi.ProductName,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice
                }).ToList() ?? []
            };
        }
    }
}
