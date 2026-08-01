using Microsoft.EntityFrameworkCore;

namespace WebAPI.Features.Common.CarModel.Admin.CreateCarModel;

public class CreateCarModelEndpoint(CardinarDbContext context)
    : Endpoint<CreateCarModelRequest, CreateCarModelResponse>
{
    public override void Configure()
    {
        Post("v1/admin/car-models/create");
        Tags("Admin");
        AllowAnonymous();
        Options(x => x.WithTags("CarModels"));
    }

    public override async Task<CreateCarModelResponse> ExecuteAsync(CreateCarModelRequest req, CancellationToken ct)
    {
        var make = await context.CarMakes.FirstOrDefaultAsync(x => x.Id == req.CarMakeId, ct);

        if (make is null)
            throw new Exception("Car make not found.");

        var alreadyExists = await context.CarModels
            .AnyAsync(x => x.Title == req.Title, ct);

        if (alreadyExists)
            throw new Exception("Car model already exists.");

        var model = new Entities.CarModel
        {
            CarMakeId = req.CarMakeId,
            Title = req.Title
        };

        context.CarModels.Add(model);
        await context.SaveChangesAsync(ct);

        return new CreateCarModelResponse
        {
            Id = model.Id,
            CarMakeId = model.CarMakeId,
            Title = model.Title
        };
    }
}

