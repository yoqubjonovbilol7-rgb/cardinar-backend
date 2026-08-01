using Microsoft.EntityFrameworkCore;

namespace WebAPI.Features.Common.CarModel.Admin.UpdateCarModel;

public class UpdateCarModelEndpoint(CardinarDbContext context)
    : Endpoint<UpdateCarModelRequest, UpdateCarModelResponse>
{
    public override void Configure()
    {
        Patch("v1/admin/car-models/update");
        Tags("Admin");
        AllowAnonymous();
        Options(x => x.WithTags("CarModels"));
    }

    public override async Task<UpdateCarModelResponse> ExecuteAsync(UpdateCarModelRequest req, CancellationToken ct)
    {
        var model = await context.CarModels.FirstOrDefaultAsync(x => x.Id == req.Id, ct);

        if (model is null)
            throw new Exception("Car model not found.");

        var make = await context.CarMakes.FirstOrDefaultAsync(x => x.Id == req.CarMakeId, ct);

        if (make is null)
            throw new Exception("Car make not found.");

        var alreadyExists = await context.CarModels
            .AnyAsync(x => x.Title == req.Title && x.Id != req.Id, ct);

        if (alreadyExists)
            throw new Exception("Car model already exists.");

        model.Title = req.Title;
        model.CarMakeId = req.CarMakeId;

        await context.SaveChangesAsync(ct);

        return new UpdateCarModelResponse
        {
            Id = model.Id,
            CarMakeId = model.CarMakeId,
            Title = model.Title
        };
    }
}

