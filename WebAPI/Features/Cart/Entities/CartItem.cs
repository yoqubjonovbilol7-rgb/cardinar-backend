using WebAPI.Features.Products.Entities;

namespace WebAPI.Features.Cart.Entities;

public class CartItem : BaseEntity
{
    public int ProductId { get; set; }

    public Product Product { get; set; } = null!;

    public int ArticulId { get; set; }

    public Articul Articul { get; set; } = null!;

    public int Quantity { get; set; } = 1;
}