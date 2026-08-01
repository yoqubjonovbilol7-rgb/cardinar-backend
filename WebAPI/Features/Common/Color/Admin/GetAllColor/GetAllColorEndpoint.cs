using Microsoft.EntityFrameworkCore;

namespace WebAPI.Features.Common.Color.Admin.GetAllColor;

public class GetAllColorEndpoint(CardinarDbContext context)
    : Endpoint<GetAllColorRequest, PaginatedResponse<GetAllColorResponse>>
{
    public override void Configure()
    {
        Get("v1/admin/colors/get-all");
        Policies("Admin");
        Tags("Admin");
        Options(opts => opts.WithTags("Colors"));
    }

    public override async Task<PaginatedResponse<GetAllColorResponse>> ExecuteAsync(GetAllColorRequest req, CancellationToken ct)
    {
        var currentPage = req.Page ?? 1;
        var take = req.Size ?? 10;
        var skip = (currentPage - 1) * take;

        var query = context.Colors.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(req.Search))
        {
            query = query.Where(x => 
                EF.Functions.ILike(x.Title, $"%{req.Search}%") ||
                EF.Functions.ILike(x.ColorCode, $"%{req.Search}%"));
        }

        var totalCount = await query.CountAsync(ct);
        var totalPages = (int)Math.Ceiling((double)totalCount / take);

        var data = await query
            .OrderBy(x => x.Id)
            .Select(GetAllColorResponse.Project)
            .Skip(skip)
            .Take(take)
            .ToArrayAsync(ct);

        return PaginatedResponse<GetAllColorResponse>.BuildFrom(
            totalCount,
            totalPages,
            currentPage,
            data);
    }
}

