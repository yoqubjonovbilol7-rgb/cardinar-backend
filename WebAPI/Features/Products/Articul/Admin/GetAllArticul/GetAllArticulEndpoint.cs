using Microsoft.EntityFrameworkCore;

namespace WebAPI.Features.Products.Articul.Admin.GetAllArticul;

public class GetAllArticulEndpoint(CardinarDbContext context)
    : Endpoint<GetAllArticulRequest, PaginatedResponse<GetAllArticulResponse>>
{
    public override void Configure()
    {
        Get("v1/admin/articuls/get-all");
        Policies("Admin");
        Tags("Admin");
        Options(opts => opts.WithTags("Articuls"));
    }

    public override async Task<PaginatedResponse<GetAllArticulResponse>> ExecuteAsync(GetAllArticulRequest req, CancellationToken ct)
    {
        var currentPage = req.Page ?? 1;
        var take = req.Size ?? 10;
        var skip = (currentPage - 1) * take;

        var query = context.Articuls.AsNoTracking().Include(x => x.Product).Include(x => x.CarModel).AsQueryable();

        if (req.ProductId.HasValue)
        {
            query = query.Where(x => x.ProductId == req.ProductId.Value);
        }

        if (req.CarModelId.HasValue)
        {
            query = query.Where(x => x.CarModelId == req.CarModelId.Value);
        }

        var totalCount = await query.CountAsync(ct);
        var totalPages = (int)Math.Ceiling((double)totalCount / take);

        var data = await query
            .OrderBy(x => x.Id)
            .Select(GetAllArticulResponse.Project)
            .Skip(skip)
            .Take(take)
            .ToArrayAsync(ct);

        return PaginatedResponse<GetAllArticulResponse>.BuildFrom(
            totalCount,
            totalPages,
            currentPage,
            data);
    }
}

