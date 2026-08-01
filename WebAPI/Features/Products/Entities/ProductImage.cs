namespace WebAPI.Features.Products.Entities;

public class ProductImage : BaseEntity
{
  
    public int ProductId { get; set; }

    public string ImagePath { get; set; } = null!;

    public int Position { get; set; }

    public Product Product { get; set; } = null!;
}