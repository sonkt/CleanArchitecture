using GbLib.BuildingBlock.Application.CQRS;
using GbLib.BuildingBlock.Domain.Interfaces;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Interfaces;

namespace OrderManagement.Application.Orders.Commands
{
    public class CreateOrderCommandHandler : CommandHandler<CreateOrderCommand, Guid>
    {
        private readonly IOrderCommandRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CreateOrderCommandHandler(IOrderCommandRepository orderRepository,IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }
        public override async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderItems = request.OrderInfo.OrderItems.Select(oi => new OrderItem(oi.ProductName, oi.Quantity, oi.UnitPrice)).ToList();
            var order = new Order(request.OrderInfo.CustomerName, request.OrderInfo.CustomerEmail, request.OrderInfo.CustomerPhone, request.OrderInfo.CustomerAddress, orderItems);
            order.OrderStatus = OrderStatus.Pending;

            await _orderRepository.AddAsync(order);
            await _unitOfWork.SaveChangesAsync();
            return order.Id;
        }
    }
}
