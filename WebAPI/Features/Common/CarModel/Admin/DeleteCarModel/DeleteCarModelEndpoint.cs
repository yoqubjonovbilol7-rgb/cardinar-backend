using Microsoft.EntityFrameworkCore;

namespace WebAPI.Features.Common.CarModel.Admin.DeleteCarModel;

public class DeleteCarModelEndpoint(CardinarDbContext context)
    : Endpoint<DeleteCarModelRequest, EmptyResponse>
{
    public override void Configure()
    {
        Delete("v1/admin/car-models/delete/{Id:int}");
        Tags("Admin");
        AllowAnonymous();
        Options(x => x.WithTags("CarModels"));
    }

    public override async Task<EmptyResponse> ExecuteAsync(DeleteCarModelRequest req, CancellationToken ct)
    {
        var model = await context.CarModels.FirstOrDefaultAsync(x => x.Id == req.Id, ct);

        if (model is null)
            throw new Exception("Car model not found");

        context.CarModels.Remove(model);

        await context.SaveChangesAsync(ct);

        return EmptyResponse.Instance;
    }
}

