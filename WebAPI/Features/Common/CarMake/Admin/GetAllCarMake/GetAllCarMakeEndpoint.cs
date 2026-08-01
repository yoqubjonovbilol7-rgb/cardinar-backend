using Microsoft.EntityFrameworkCore;

namespace WebAPI.Features.Common.CarMake.Admin.GetAllCarMake;

public class GetAllCarMakesEndpoint(CardinarDbContext context)
    : Endpoint<GetAllCarMakeRequest, PaginatedResponse<GetAllCarMakesResponse>>
{
    public override void Configure()
    {
        Get("v1/admin/car-makes/get-all");
        Policies("Admin");
        Tags("Admin");
        Options(opts => opts.WithTags("CarMakes"));
    }

    public override async Task<PaginatedResponse<GetAllCarMakesResponse>> ExecuteAsync(GetAllCarMakeRequest req, CancellationToken ct)
    {
        var currentPage = req.Page ?? 1;
        var take = req.Size ?? 10;
        var skip = (currentPage - 1) * take;

        var query = context.CarMakes.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(req.Search))
        {
            query = query.Where(x =>
                EF.Functions.ILike(x.Title, $"%{req.Search}%"));
        }

        var totalCount = await query.CountAsync(ct);
        var totalPages = (int)Math.Ceiling((double)totalCount / take);

        var data = await query
            .OrderBy(x => x.Id)
            .Select(GetAllCarMakesResponse.Project)
            .Skip(skip)
            .Take(take)
            .ToArrayAsync(ct);

        return PaginatedResponse<GetAllCarMakesResponse>.BuildFrom(
            totalCount,
            totalPages,
            currentPage,
            data);
    }
}