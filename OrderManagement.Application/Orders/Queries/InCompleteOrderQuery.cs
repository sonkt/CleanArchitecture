using GbLib.BuildingBlock.Application.CQRS;
using OrderManagement.Application.Orders.Dtos;

namespace OrderManagement.Application.Orders.Queries;

public class InCompleteOrderQuery:IQuery<(List<OrderDto>,int)>
{
    public int Page { get; set; }
    public int Size { get; set; }
}