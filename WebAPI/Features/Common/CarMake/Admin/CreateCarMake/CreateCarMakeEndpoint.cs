using Microsoft.EntityFrameworkCore;

namespace WebAPI.Features.Common.CarMake.Admin.CreateCarMake;

public class CreateCarMakeEndpoint(CardinarDbContext context)
    : Endpoint<CreateCarMakeRequest, CreateCarMakeResponse>
{
    public override void Configure()
    {
        Post("v1/admin/car-makes/create");
        Tags("Admin");
        AllowAnonymous();
        Options(x => x.WithTags("CarMakes"));
    }

    public override async Task<CreateCarMakeResponse> ExecuteAsync(CreateCarMakeRequest req, CancellationToken ct)
    {
        var alreadyExists = await context.CarMakes
            .AnyAsync(x => x.Title == req.Title, ct);

        if (alreadyExists)
        {
            throw new Exception("Car make already exists.");
        }

        var carMake = new Entities.CarMake
        {
            Title = req.Title
        };

        context.CarMakes.Add(carMake);
        await context.SaveChangesAsync(ct);

        return new CreateCarMakeResponse
        {
            Id = carMake.Id,
            Title = carMake.Title
        };
    }
}