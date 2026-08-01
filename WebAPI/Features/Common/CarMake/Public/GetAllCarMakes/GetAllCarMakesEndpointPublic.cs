using Microsoft.EntityFrameworkCore;
using WebAPI.Features.Common.CarMake.Public.GetAllCarMakes;
using WebApi.Features.Common.CarMakes.Public.GetAllCarMakes;

namespace WebAPI.Features.Common.CarMake.Public.GetAllCarMakes;

public class GetAllCarMakesEndpointPublic(CardinarDbContext context)
    : Endpoint<GetAllCarMakesRequestPublic, PaginatedResponse<GetAllCarMakesResponsePublic>>
{
    public override void Configure()
    {
        Get("v1/public/carMakes/get-all-carMakes");
        Policies("Public");
        Tags("Public");
        Options(opts => opts.WithTags("CarMakes"));
    }

    public override async Task<PaginatedResponse<GetAllCarMakesResponsePublic>> ExecuteAsync(GetAllCarMakesRequestPublic req,
        CancellationToken ct)
    {
        var currenatPage = req.Page ?? 1;
        var take = req.Size ?? 10;
        var skip = (currenatPage - 1) * take;

        var query = context.CarMakes.AsNoTracking();

        if (!string.IsNullOrEmpty(req.Search))
            query = query.Where(u => EF.Functions.ILike(u.Title, $"%{req.Search}%"));

        var totalCount = await query.CountAsync(ct);
        var totalPages = (int)Math.Ceiling((double)totalCount / (req.Size ?? 10));
        var data = await query.Select(GetAllCarMakesResponsePublic.Project).Skip(skip).Take(take).ToArrayAsync(ct);

        return PaginatedResponse<GetAllCarMakesResponsePublic>.BuildFrom(totalCount, totalPages, currenatPage, data);
    }
}