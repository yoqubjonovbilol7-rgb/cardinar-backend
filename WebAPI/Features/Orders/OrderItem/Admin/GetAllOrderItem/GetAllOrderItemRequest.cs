namespace WebAPI.Features.Orders.OrderItem.Admin.GetAllOrderItem;

public class GetAllOrderItemRequest : PaginatedRequest
{
    public int? OrderId { get; set; }
    public int? ProductId { get; set; }
}

