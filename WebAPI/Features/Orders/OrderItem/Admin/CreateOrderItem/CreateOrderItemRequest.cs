namespace WebAPI.Features.Orders.OrderItem.Admin.CreateOrderItem;

public class CreateOrderItemRequest
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int ArticulId { get; set; }
    public int Quantity { get; set; } = 1;
}

