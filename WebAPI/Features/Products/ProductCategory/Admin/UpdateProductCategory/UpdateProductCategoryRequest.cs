namespace WebAPI.Features.Products.ProductCategory.Admin.UpdateProductCategory;

public class UpdateProductCategoryRequest
{
    public int Id { get; set; }
    public string Title { get; set; } = String.Empty;
}