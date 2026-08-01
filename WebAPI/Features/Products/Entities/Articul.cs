using WebAPI.Features.Common.Entities;
using WebAPI.Features.Orders.Entities;

namespace WebAPI.Features.Products.Entities;

public class Articul : BaseEntity
{
    public int ProductId { get; set; }

    public int CarModelId { get; set; }
    
    public Product Product { get; set; } = null!;

    public CarModel CarModel { get; set; } = null!;

    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}