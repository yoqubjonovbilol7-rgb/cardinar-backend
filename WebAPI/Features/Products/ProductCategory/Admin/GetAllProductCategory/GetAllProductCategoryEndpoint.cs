using Microsoft.EntityFrameworkCore;

namespace WebAPI.Features.Products.ProductCategory.Admin.GetAllProductCategory;

public class GetAllProductCategoryEndpoint(CardinarDbContext context)
    : Endpoint<GetAllProductCategoryRequest, PaginatedResponse<GetAllProductCategoryResponse>>
{
    public override void Configure()
    {
        Get("v1/admin/product-categories/get-all");
        Policies("Admin");
        Tags("Admin");
        Options(options => options.WithTags("ProductCategories"));
    }

    public override async Task<PaginatedResponse<GetAllProductCategoryResponse>> ExecuteAsync(
        GetAllProductCategoryRequest req,
        CancellationToken ct)
    {
        var currentPage = req.Page ?? 1;
        var pageSize = req.Size ?? 10;
        var skip = (currentPage - 1) * pageSize;

        IQueryable<Entities.ProductCategory> query = context.ProductCategories
            .AsNoTracking();

        if (!string.IsNullOrWhiteSpace(req.Search))
        {
            query = query.Where(x =>
                EF.Functions.ILike(x.Title, $"%{req.Search}%"));
        }
        
        var totalCount = await query.CountAsync(ct);

        var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
        var data = await query
            .OrderBy(x => x.Id)
            .Skip(skip)
            .Take(pageSize)
            .Select(GetAllProductCategoryResponse.Project)
            .ToArrayAsync(ct);

        return PaginatedResponse<GetAllProductCategoryResponse>.BuildFrom(
            totalCount,
            totalPages,
            currentPage,
            data
        );
    }
}