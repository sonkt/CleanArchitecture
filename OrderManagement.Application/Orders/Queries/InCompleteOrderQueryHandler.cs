using GbLib.BuildingBlock.Application.CQRS;
using OrderManagement.Application.Orders.Dtos;
using OrderManagement.Application.Specifications;
using OrderManagement.Domain.Interfaces;

namespace OrderManagement.Application.Orders.Queries;

public class InCompleteOrderQueryHandler : QueryHandler<InCompleteOrderQuery,(List<OrderDto>,int)>
{
    private readonly IOrderQueryRepository _orderRepository;
    public InCompleteOrderQueryHandler(IOrderQueryRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    public override async Task<(List<OrderDto>,int)> Handle(InCompleteOrderQuery request, CancellationToken cancellationToken)
    {
        var specIncomplete = new IncompleteOrdersSpecification(request.Page, request.Size);
        var result =await _orderRepository.ListAsyncWithPaging(specIncomplete);
        var items = result.Items.Select(m=>new OrderDto
        {
            CustomerAddress = m.CustomerAddress,
            CustomerEmail = m.CustomerEmail,
            CustomerName = m.CustomerName,
            CustomerPhone = m.CustomerPhone,
            OrderItems = m.OrderItems.Select(i=>new OrderItemDto
            {
                ProductName = i.ProductName,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice
            }).ToList()
        }).ToList();
        return (items,result.TotalCount);
    }
}