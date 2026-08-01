using Microsoft.EntityFrameworkCore;

namespace WebAPI.Features.Common.CarModel.Admin.GetOneCarModel;

public class GetOneCarModelEndpoint(CardinarDbContext context) : Endpoint<GetOneCarModelRequest, GetOneCarModelResponse>
{
    public override void Configure()
    {
        Get("v1/admin/car-models/get-one-car-model/{Id:int}");
        Policies("Admin");
        Tags("Admin");
        Options(opts => opts.WithTags("CarModels"));
    }

    public override async Task<GetOneCarModelResponse> ExecuteAsync(GetOneCarModelRequest req, CancellationToken ct)
    {
        var carModels = await context.CarModels.SingleOrDefaultAsync(cm => cm.Id == req.Id, ct);

        if (carModels == null)
            throw new Exception("Car models with given id does not exists.");

        return GetOneCarModelResponse.FromEntity(carModels);
    }
}