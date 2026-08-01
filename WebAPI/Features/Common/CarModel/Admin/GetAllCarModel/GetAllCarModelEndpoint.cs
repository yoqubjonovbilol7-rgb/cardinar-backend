using Microsoft.EntityFrameworkCore;

namespace WebAPI.Features.Common.CarModel.Admin.GetAllCarModel;

public class GetAllCarModelsEndpoint(CardinarDbContext context)
    : Endpoint<GetAllCarModelRequest, PaginatedResponse<GetAllCarModelsResponse>>
{
    public override void Configure()
    {
        Get("v1/admin/car-models/get-all");
        Policies("Admin");
        Tags("Admin");
        Options(opts => opts.WithTags("CarModels"));
    }

    public override async Task<PaginatedResponse<GetAllCarModelsResponse>> ExecuteAsync(GetAllCarModelRequest req, CancellationToken ct)
    {
        var currentPage = req.Page ?? 1;
        var take = req.Size ?? 10;
        var skip = (currentPage - 1) * take;

        var query = context.CarModels.AsNoTracking().Include(x => x.CarMake).AsQueryable();

        if (req.CarMakeId.HasValue)
        {
            query = query.Where(x => x.CarMakeId == req.CarMakeId.Value);
        }

        if (!string.IsNullOrWhiteSpace(req.Search))
        {
            query = query.Where(x => EF.Functions.ILike(x.Title, $"%{req.Search}%"));
        }

        var totalCount = await query.CountAsync(ct);
        var totalPages = (int)Math.Ceiling((double)totalCount / take);

        var data = await query
            .OrderBy(x => x.Id)
            .Select(GetAllCarModelsResponse.Project)
            .Skip(skip)
            .Take(take)
            .ToArrayAsync(ct);

        return PaginatedResponse<GetAllCarModelsResponse>.BuildFrom(
            totalCount,
            totalPages,
            currentPage,
            data);
    }
}

