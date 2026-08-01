using Microsoft.EntityFrameworkCore;

namespace WebAPI.Features.Constructor.Materials.Admin.GetAllMaterial;

public class GetAllMaterialEndpoint(CardinarDbContext context)
    : Endpoint<GetAllMaterialRequest, PaginatedResponse<GetAllMaterialResponse>>
{
    public override void Configure()
    {
        Get("v1/admin/materials/get-all");
        Policies("Admin");
        Tags("Admin");
        Options(opts => opts.WithTags("Materials"));
    }

    public override async Task<PaginatedResponse<GetAllMaterialResponse>> ExecuteAsync(GetAllMaterialRequest req, CancellationToken ct)
    {
        var currentPage = req.Page ?? 1;
        var take = req.Size ?? 10;
        var skip = (currentPage - 1) * take;

        var query = context.Materials.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(req.Search))
        {
            query = query.Where(x => EF.Functions.ILike(x.Title, $"%{req.Search}%"));
        }

        var totalCount = await query.CountAsync(ct);
        var totalPages = (int)Math.Ceiling((double)totalCount / take);

        var data = await query
            .OrderBy(x => x.Id)
            .Select(GetAllMaterialResponse.Project)
            .Skip(skip)
            .Take(take)
            .ToArrayAsync(ct);

        return PaginatedResponse<GetAllMaterialResponse>.BuildFrom(
            totalCount,
            totalPages,
            currentPage,
            data);
    }
}

