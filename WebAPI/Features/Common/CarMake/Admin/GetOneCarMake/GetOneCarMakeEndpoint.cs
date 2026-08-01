using Microsoft.EntityFrameworkCore;

namespace WebAPI.Features.Common.CarMake.Admin.GetOneCarMake;

public class GetOneCarMakeEndpoint(CardinarDbContext context) : Endpoint<GetOneCarMakeRequest, GetOneCarMakeResponse>
{
    public override void Configure()
    {
        Get("v1/admin/car-makes/get-one-car-make/{Id:int}");
        Policies("Admin");
        Tags("Admin");
        Options(opts => opts.WithTags("CarMakes"));
    }

    public override async Task<GetOneCarMakeResponse> ExecuteAsync(GetOneCarMakeRequest req, CancellationToken ct)
    {
        var carMakes = await context.CarMakes.SingleOrDefaultAsync(c => c.Id == req.Id, ct);

        if (carMakes is null)
            throw new Exception("Car makes with given id does not exists.");

        return new GetOneCarMakeResponse
        {
            Id = carMakes.Id,
            Title = carMakes.Title
        };
    }
}