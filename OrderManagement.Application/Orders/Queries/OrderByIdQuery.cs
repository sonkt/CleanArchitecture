using GbLib.BuildingBlock.Application.CQRS;
using OrderManagement.Application.Orders.Dtos;

namespace OrderManagement.Application.Orders.Queries
{
    public class OrderByIdQuery:IQuery<OrderDto>
    {
        public Guid Id;
    }
}
