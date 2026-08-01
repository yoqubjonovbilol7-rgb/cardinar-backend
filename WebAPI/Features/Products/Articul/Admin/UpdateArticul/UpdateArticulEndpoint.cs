using Microsoft.EntityFrameworkCore;

namespace WebAPI.Features.Products.Articul.Admin.UpdateArticul;

public class UpdateArticulEndpoint(CardinarDbContext context)
    : Endpoint<UpdateArticulRequest, UpdateArticulResponse>
{
    public override void Configure()
    {
        Patch("v1/admin/articuls/update");
        Tags("Admin");
        AllowAnonymous();
        Options(x => x.WithTags("Articuls"));
    }

    public override async Task<UpdateArticulResponse> ExecuteAsync(UpdateArticulRequest req, CancellationToken ct)
    {
        var articul = await context.Articuls.FirstOrDefaultAsync(x => x.Id == req.Id, ct);

        if (articul is null)
            throw new Exception("Articul not found.");

        var product = await context.Products.FirstOrDefaultAsync(x => x.Id == req.ProductId, ct);

        if (product is null)
            throw new Exception("Product not found.");

        var carModel = await context.CarModels.FirstOrDefaultAsync(x => x.Id == req.CarModelId, ct);

        if (carModel is null)
            throw new Exception("Car model not found.");

        articul.ProductId = req.ProductId;
        articul.CarModelId = req.CarModelId;

        await context.SaveChangesAsync(ct);

        return new UpdateArticulResponse
        {
            Id = articul.Id,
            ProductId = articul.ProductId,
            CarModelId = articul.CarModelId
        };
    }
}

