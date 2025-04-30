
namespace OrderManagement.Application.Orders.Dtos
{
    public class OrderDto
    {
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerAddress { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
    }
}
