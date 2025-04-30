using GbLib.BuildingBlock.Application.CQRS;
using OrderManagement.Application.Orders.Dtos;

namespace OrderManagement.Application.Orders.Commands;

public class CreateOrderCommand : ICommand<Guid>
{
    public OrderDto OrderInfo { get; set; }
}
