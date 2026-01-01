using GbLib.BuildingBlock.Domain.Interfaces;

namespace OrderManagement.Application.Services;

public class CurrentUserProvider:ICurrentUserProvider
{
    public string GetCurrentUserId()
    {
        return "sonkt";
    }
}