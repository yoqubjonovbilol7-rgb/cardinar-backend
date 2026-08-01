using System.Linq.Expressions;

namespace WebAPI.Features.Cart.CartItem.Admin.GetAllCartItem;

public class GetAllCartItemResponse
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string ProductTitle { get; set; } = string.Empty;
    public int ArticulId { get; set; }
    public int Quantity { get; set; }

    public static Expression<Func<Entities.CartItem, GetAllCartItemResponse>> Project =>
        x => new GetAllCartItemResponse
        {
            Id = x.Id,
            ProductId = x.ProductId,
            ProductTitle = x.Product.Title,
            ArticulId = x.ArticulId,
            Quantity = x.Quantity
        };
}

