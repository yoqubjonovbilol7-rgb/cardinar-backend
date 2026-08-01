using System.Linq.Expressions;

namespace WebAPI.Features.Products.ProductCategory.Admin.GetAllProductCategory;

public class GetAllProductCategoryResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;

    public static Expression<Func<Entities.ProductCategory, GetAllProductCategoryResponse>> Project =>
        c => new GetAllProductCategoryResponse
        {
            Id = c.Id,
            Title = c.Title
        };
}