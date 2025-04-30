using GbLib.BuildingBlock.Domain.Specifications;
using OrderManagement.Domain.Entities;

namespace OrderManagement.Application.Specifications;

public class IncompleteOrdersSpecification : BaseSpecification<Order>
{
    public IncompleteOrdersSpecification(int page = 1, int size = 10)
    {
        var skip = (page - 1) * size;
        Criteria = m => m.OrderStatus==OrderStatus.Pending;
        ApplyPaging(skip, size);
        AddInclude(o => o.OrderItems);
        ApplyOrderByDescending(o => o.CustomerPhone);
        ApplyAsNoTracking();
    }
}
