using Microsoft.EntityFrameworkCore;

namespace WebAPI.Features.Common.Color.Public.GetAllColor;

public class GetAllColorEndpointPublic(CardinarDbContext context)
    : Endpoint<GetAllColorRequestPublic, PaginatedResponse<GetAllColorResponsePublic>>
{
    public override void Configure()
    {
        Get("v1/public/colors/get-all-colors");
        Policies("Public");
        Tags("Public");
        Options(opts => opts.WithTags("Colors"));
    }

    public override async Task<PaginatedResponse<GetAllColorResponsePublic>> ExecuteAsync(
        GetAllColorRequestPublic req,
        CancellationToken ct)
    {
        var currentPage = req.Page ?? 1;
        var take = req.Size ?? 10;
        var skip = (currentPage - 1) * take;

        var query = context.Colors.AsNoTracking();

        if (!string.IsNullOrEmpty(req.Search))
            query = query.Where(u => EF.Functions.ILike(u.Title, $"%{req.Search}%") ||
                                     EF.Functions.ILike(u.ColorCode, $"%{req.Search}%"));

        var totalCount = await query.CountAsync(ct);
        var totalPages = (int)Math.Ceiling((double)totalCount / take);
        var data = await query.Select(GetAllColorResponsePublic.Project).Skip(skip).Take(take).ToArrayAsync(ct);
        return PaginatedResponse<GetAllColorResponsePublic>.BuildFrom(totalCount, totalPages, currentPage, data);
    }
}