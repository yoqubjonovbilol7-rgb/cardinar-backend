using Microsoft.EntityFrameworkCore;

namespace WebAPI.Features.Common.CarMake.Admin.UpdateCarMake;

public class UpdateCarMakeEndpoint(CardinarDbContext context)
    : Endpoint<UpdateCarMakeRequest, UpdateCarMakeResponse>
{
    public override void Configure()
    {
        Patch("v1/admin/car-makes/update");
        Tags("Admin");
        AllowAnonymous();
        Options(x => x.WithTags("CarMakes"));
    }

    public override async Task<UpdateCarMakeResponse> ExecuteAsync(UpdateCarMakeRequest req, CancellationToken ct)
    {
        var carMake = await context.CarMakes
            .FirstOrDefaultAsync(x => x.Id == req.Id, ct);

        if (carMake is null)
        {
            throw new Exception("Car make not found.");
        }

        var alreadyExists = await context.CarMakes
            .AnyAsync(x => x.Title == req.Title && x.Id != req.Id, ct);

        if (alreadyExists)
        { 
            throw new Exception("Car make already exists.");
        }

        carMake.Title = req.Title;

        await context.SaveChangesAsync(ct);

        return new UpdateCarMakeResponse
        {
            Id = carMake.Id,
            Title = carMake.Title
        };
    }
}