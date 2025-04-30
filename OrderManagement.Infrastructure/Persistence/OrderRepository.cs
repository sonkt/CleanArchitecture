using GbLib.BuildingBlock.Domain.Interfaces;
using GbLib.BuildingBlock.Infrastructure.Persistence;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Interfaces;

namespace OrderManagement.Infrastructure.Persistence;

public class OrderQueryRepository(AppDbContext appDbContext) : EfQueryRepository<Order,Guid>(appDbContext), IOrderQueryRepository
{}

public class OrderCommandRepository(AppDbContext appDbContext, IUnitOfWork unitOfWork) : EfCommandRepository<Order,Guid>(appDbContext,unitOfWork), IOrderCommandRepository
{}