namespace WebAPI.Features.Cart.CartItem.Admin.GetAllCartItem;

public class GetAllCartItemRequest : PaginatedRequest
{
    public int? ProductId { get; set; }
}

