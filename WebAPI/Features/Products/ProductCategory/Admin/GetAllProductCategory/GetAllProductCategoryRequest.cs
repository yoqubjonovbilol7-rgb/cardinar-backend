namespace WebAPI.Features.Products.ProductCategory.Admin.GetAllProductCategory;

public class GetAllProductCategoryRequest : PaginatedRequest
{
    public string? Search { get; set; }
    public bool? IsActive { get; set; }
}