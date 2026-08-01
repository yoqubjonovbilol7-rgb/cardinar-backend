using WebAPI.Features.Products.Entities;

namespace WebAPI.Features.Orders.Entities;

public class OrderItem  : BaseEntity
{
    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public int ArticulId { get; set; }

    public int Quantity { get; set; } = 1;
    
    public Order Order { get; set; } = null!;

    public Product Product { get; set; } = null!;

    public Articul Articul { get; set; } = null!;
}