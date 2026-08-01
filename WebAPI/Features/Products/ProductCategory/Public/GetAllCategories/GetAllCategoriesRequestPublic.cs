namespace WebAPI.Features.Products.ProductCategory.Public.GetAllCategories;

public class GetAllCategoriesRequestPublic : PaginatedRequest
{
    public string? Search { get; set; }
}