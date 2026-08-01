using WebAPI.Features.Products.ProductCategory.Admin.GetAllProductCategory;

namespace WebAPI.Core;

public class PaginatedResponse<T>
{
  public int TotalCount { get; set; }
  public int TotalPages { get; set; }
  public int CurrentPage { get; set; }
  public bool HasNext { get; set; }
  public bool HasPrevious { get; set; }
  public ICollection<T> Data { get; set; } = [];

  public static PaginatedResponse<T> BuildFrom(int totalCount, int totalPages, int currentPage, ICollection<T> data) => new()
  {
    TotalCount = totalCount,
    TotalPages = totalPages,
    CurrentPage = currentPage,
    HasNext = currentPage < totalPages,
    HasPrevious = currentPage > 1,
    Data = data
  };
  
}