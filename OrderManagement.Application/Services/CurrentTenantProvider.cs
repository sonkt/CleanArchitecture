using GbLib.BuildingBlock.Domain.Interfaces;

namespace OrderManagement.Application.Services;

public class CurrentTenantProvider:ICurrentTenantProvider
{
    public Guid GetCurrentTenantId()
    {
        return Guid.NewGuid();
    }
}