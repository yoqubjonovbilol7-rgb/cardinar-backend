using Microsoft.EntityFrameworkCore;

namespace WebAPI.Features.Products.Articul.Admin.CreateArticul;

public class CreateArticulEndpoint(CardinarDbContext context)
    : Endpoint<CreateArticulRequest, CreateArticulResponse>
{
    public override void Configure()
    {
        Post("v1/admin/articuls/create");
        Tags("Admin");
        AllowAnonymous();
        Options(x => x.WithTags("Articuls"));
    }

    public override async Task<CreateArticulResponse> ExecuteAsync(CreateArticulRequest req, CancellationToken ct)
    {
        var product = await context.Products.FirstOrDefaultAsync(x => x.Id == req.ProductId, ct);

        if (product is null)
            throw new Exception("Product not found.");

        var carModel = await context.CarModels.FirstOrDefaultAsync(x => x.Id == req.CarModelId, ct);

        if (carModel is null)
            throw new Exception("Car model not found.");

        var articul = new Entities.Articul
        {
            ProductId = req.ProductId,
            CarModelId = req.CarModelId
        };

        context.Articuls.Add(articul);
        await context.SaveChangesAsync(ct);

        return new CreateArticulResponse
        {
            Id = articul.Id,
            ProductId = articul.ProductId,
            CarModelId = articul.CarModelId
        };
    }
}

