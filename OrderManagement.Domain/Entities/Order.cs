using GbLib.BuildingBlock.Domain.Entities;

namespace OrderManagement.Domain.Entities;

public class Order : AggregateRoot<Guid>
{
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerPhone { get; set; }
    public string CustomerAddress { get; set; }
    public virtual List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public decimal TotalAmount => OrderItems.Sum(x => x.TotalPrice);
    public OrderStatus OrderStatus { get; set; }

    private Order() { }

    public Order(string customerName, string customerEmail, string customerPhone, string customerAddress, List<OrderItem> items)
    {
        Id = Guid.NewGuid();
        CustomerName = customerName;
        CustomerEmail = customerEmail;
        CustomerPhone = customerPhone;
        CustomerAddress = customerAddress;
        OrderItems = items;
    }
}

public sealed class OrderItem : Entity<Guid>
{
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; private set; }
    public decimal TotalPrice => Quantity * UnitPrice;
    public Guid OrderId { get; set; }
    public Order Order { get; set; }

    private OrderItem() { }

    public OrderItem(string productName, int quantity, decimal unitPrice)
    {
        Id= Guid.NewGuid();
        ProductName = productName;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }
}

public enum OrderStatus
{
    Pending,
    Confirmed,
    Shipped,
}