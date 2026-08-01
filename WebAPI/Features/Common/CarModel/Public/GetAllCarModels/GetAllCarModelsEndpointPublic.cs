using Microsoft.EntityFrameworkCore;

namespace WebAPI.Features.Common.CarModel.Public.GetAllCarModels;

public class GetAllCarModelsEndpointPublic(CardinarDbContext context)
    : Endpoint<GetAllCarModelsRequestPublic, PaginatedResponse<GetAllCarModelsResponsePublic>>
{
    public override void Configure()
    {
        Get("v1/public/car-models/get-all-car-models");
        Policies("Public");
        Tags("Public");
        Options(opts => opts.WithTags("CarModels"));
    }

    public override async Task<PaginatedResponse<GetAllCarModelsResponsePublic>> ExecuteAsync(
        GetAllCarModelsRequestPublic req,
        CancellationToken ct)
    {
        var currentPage = req.Page ?? 1;
        var take = req.Size ?? 10;
        var skip = (currentPage - 1) * take;

        var query = context.CarModels.AsNoTracking();

        if (!string.IsNullOrEmpty(req.Search))
            query = query.Where(cm => EF.Functions.ILike(cm.Title, $"%{req.Search}%"));

        var totalCount = await query.CountAsync(ct);
        var totalPages = (int)Math.Ceiling((double)totalCount / take);
        var data = await query.Select(GetAllCarModelsResponsePublic.Project).Skip(skip).Take(take).ToArrayAsync(ct);

        return PaginatedResponse<GetAllCarModelsResponsePublic>.BuildFrom(totalCount, totalPages, currentPage, data);
    }
}