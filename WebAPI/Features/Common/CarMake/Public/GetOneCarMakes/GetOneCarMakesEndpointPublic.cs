using Microsoft.EntityFrameworkCore;
using WebAPI;

namespace WebApi.Features.Common.CarMakes.Public.GetOneCarMakes;

public class GetOneCarMakesEndpointPublic(CardinarDbContext context)
    : Endpoint<GetOneCarMakesRequestPublic, GetOneCarMakesResponsePublic>
{
    public override void Configure()
    {
        Get("v1/public/CarMakes/get-one-CarMakes/{Id:int}");
        Policies("Public");
        Tags("Public");
        Options(opts => opts.WithTags("CarMakes"));
    }

    public override async Task<GetOneCarMakesResponsePublic> ExecuteAsync(GetOneCarMakesRequestPublic req,
        CancellationToken ct)
    {
        var carMakes = await context.CarMakes.SingleOrDefaultAsync(c => c.Id == req.Id, ct);

        if (carMakes == null)
            throw new Exception("Car makes with given id does not exists.");

        return new GetOneCarMakesResponsePublic
        {
            Id = carMakes.Id,
            Title = carMakes.Title
        };
    }
}