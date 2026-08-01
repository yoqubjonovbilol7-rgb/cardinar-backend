using System.Linq.Expressions;

namespace WebAPI.Features.Orders.OrderItem.Admin.GetAllOrderItem;

public class GetAllOrderItemResponse
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public string ProductTitle { get; set; } = string.Empty;
    public int ArticulId { get; set; }
    public int Quantity { get; set; }

    public static Expression<Func<Entities.OrderItem, GetAllOrderItemResponse>> Project =>
        x => new GetAllOrderItemResponse
        {
            Id = x.Id,
            OrderId = x.OrderId,
            ProductId = x.ProductId,
            ProductTitle = x.Product.Title,
            ArticulId = x.ArticulId,
            Quantity = x.Quantity
        };
}

