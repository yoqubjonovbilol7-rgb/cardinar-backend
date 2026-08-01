using System.Linq.Expressions;
using WebAPI.Core.Enums;

namespace WebAPI.Features.Products.ProductCategory.Public.GetAllCategories;

public class GetAllCategoriesResponsePublic
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public static Expression<Func<Entities.ProductCategory, GetAllCategoriesResponsePublic>> Project = u => new GetAllCategoriesResponsePublic
    {
        Id = u.Id,
        Title = u.Title,
        CreatedAt = u.CreatedAt,
        UpdatedAt = u.UpdatedAt
    };
}