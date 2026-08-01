using Microsoft.EntityFrameworkCore;

namespace WebAPI.Features.Common.CarMake.Admin.DeleteCarMake;

public class DeleteCarMakeEndpoint(CardinarDbContext context)
    : Endpoint<DeleteCarMakeRequest, EmptyResponse>
{
    public override void Configure()
    {
        Delete("v1/admin/car-makes/delete/{Id:int}");
        Tags("Admin");
        AllowAnonymous();
        Options(x => x.WithTags("CarMakes"));
    }

    public override async Task<EmptyResponse> ExecuteAsync(DeleteCarMakeRequest req, CancellationToken ct)
    {
        var carMake = await context.CarMakes.FirstOrDefaultAsync(x => x.Id == req.Id, ct);

        if (carMake is null)
        {
            throw new Exception("CarMake not found");
        }

        context.CarMakes.Remove(carMake);

        await context.SaveChangesAsync(ct);

       return EmptyResponse.Instance;
    }
}