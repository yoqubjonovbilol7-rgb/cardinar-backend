using Microsoft.EntityFrameworkCore;
using WebApi.Features.Cars.CarModels.Public.GetOneCarModel;

namespace WebAPI.Features.Common.CarModel.Public.GetOneCarModel;

public class GetOneCarModelEndpointPublic(CardinarDbContext context)
    : Endpoint<GetOneCarModelRequestPublic, GetOneCarModelResponsePublic>
{
    public override void Configure()
    {
        Get("v1/public/car-models/get-one-car-model/{Id:int}");
        Policies("Public");
        Tags("Public");
        Options(opts => opts.WithTags("CarModels"));
    }

    public override async Task<GetOneCarModelResponsePublic> ExecuteAsync(GetOneCarModelRequestPublic req,
        CancellationToken ct)
    {
        var carModels = await context.CarModels.SingleOrDefaultAsync(cm => cm.Id == req.Id, ct);

        if (carModels == null)
            throw new Exception("Car models with given id does not exists.");

        return GetOneCarModelResponsePublic.FromEntity(carModels);
    }
}