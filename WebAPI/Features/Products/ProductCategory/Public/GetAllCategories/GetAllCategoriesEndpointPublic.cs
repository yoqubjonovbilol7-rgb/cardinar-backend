using Microsoft.EntityFrameworkCore;

namespace WebAPI.Features.Products.ProductCategory.Public.GetAllCategories;

public class GetAllCategoriesEndpointPublic(CardinarDbContext context)
    : Endpoint<GetAllCategoriesRequestPublic, PaginatedResponse<GetAllCategoriesResponsePublic>>
{
    public override void Configure()
    {
        Get("v1/public/categories/get-all-categories");
        Policies("Public");
        Tags("Public");
        Options(opts => opts.WithTags("ProductCategories"));
    }

    public override async Task<PaginatedResponse<GetAllCategoriesResponsePublic>> ExecuteAsync(GetAllCategoriesRequestPublic req,
        CancellationToken ct)
    {
        var currenatPage = req.Page ?? 1;
        var take = req.Size ?? 10;
        var skip = (currenatPage - 1) * take;

        var query = context.ProductCategories.AsNoTracking();

        if (!string.IsNullOrEmpty(req.Search))
            query = query.Where(u => EF.Functions.ILike(u.Title, $"%{req.Search}%"));

        var totalCount = await query.CountAsync(ct);
        var totalPages = (int)Math.Ceiling((double)totalCount / (req.Size ?? 10));
        var data = await query.Select(GetAllCategoriesResponsePublic.Project).Skip(skip).Take(take).ToArrayAsync(ct);

        return PaginatedResponse<GetAllCategoriesResponsePublic>.BuildFrom(totalCount, totalPages, currenatPage, data);
    }
}