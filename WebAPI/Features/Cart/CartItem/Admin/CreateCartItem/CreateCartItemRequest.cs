namespace WebAPI.Features.Cart.CartItem.Admin.CreateCartItem;

public class CreateCartItemRequest
{
    public int ProductId { get; set; }
    public int ArticulId { get; set; }
    public int Quantity { get; set; } = 1;
}

