namespace WebAPI.Features.Orders.OrderItem.Admin.UpdateOrderItem;

public class UpdateOrderItemRequest
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int ArticulId { get; set; }
    public int Quantity { get; set; }
}

