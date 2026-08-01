using Microsoft.EntityFrameworkCore;

namespace WebAPI.Features.Products.Articul.Admin.DeleteArticul;

public class DeleteArticulEndpoint(CardinarDbContext context)
    : Endpoint<DeleteArticulRequest, EmptyResponse>
{
    public override void Configure()
    {
        Delete("v1/admin/articuls/delete/{Id:int}");
        Tags("Admin");
        AllowAnonymous();
        Options(x => x.WithTags("Articuls"));
    }

    public override async Task<EmptyResponse> ExecuteAsync(DeleteArticulRequest req, CancellationToken ct)
    {
        var articul = await context.Articuls.FirstOrDefaultAsync(x => x.Id == req.Id, ct);

        if (articul is null)
            throw new Exception("Articul not found");

        context.Articuls.Remove(articul);

        await context.SaveChangesAsync(ct);

        return EmptyResponse.Instance;
    }
}

