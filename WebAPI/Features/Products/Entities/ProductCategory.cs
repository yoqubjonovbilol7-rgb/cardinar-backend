namespace WebAPI.Features.Products.Entities;

public class ProductCategory : BaseEntity
{
    public string Title { get; set; } = null!;

    public ICollection<Product> Products { get; set; } = new List<Product>();
}