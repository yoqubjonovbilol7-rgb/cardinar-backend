namespace WebAPI.Features.Cart.CartItem.Admin.UpdateCartItem;

public class UpdateCartItemRequest
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int ArticulId { get; set; }
    public int Quantity { get; set; }
}

