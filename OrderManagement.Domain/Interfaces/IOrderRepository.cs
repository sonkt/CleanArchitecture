using GbLib.BuildingBlock.Domain.Interfaces;
using OrderManagement.Domain.Entities;

namespace OrderManagement.Domain.Interfaces
{
    public interface IOrderQueryRepository: IQueryRepository<Order>
    {
    }
    public interface IOrderCommandRepository: ICommandRepository<Order>
    {
    }
}
