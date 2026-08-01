using System.Net.Mime;
using WebAPI.Core.Enums;
using WebAPI.Features.Orders.Entities;

namespace WebAPI.Features.Products.Entities;

public class Product : BaseEntity
{
    public int CategoryId { get; set; }

    public string Title { get; set; } = null!;

    public decimal Price { get; set; }

    public string? Description { get; set; }

    public Status? Status { get; set; }

    public bool IsPremium { get; set; }
    
    public ProductCategory Category { get; set; } = null!;

    public ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();

    public ICollection<Articul> Articuls { get; set; } = new List<Articul>();

    public ICollection<ProductColor> ProductColors { get; set; } = new List<ProductColor>();

    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}