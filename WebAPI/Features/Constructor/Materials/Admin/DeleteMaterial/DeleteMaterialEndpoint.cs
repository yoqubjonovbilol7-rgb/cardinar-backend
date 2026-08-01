using Microsoft.EntityFrameworkCore;

namespace WebAPI.Features.Constructor.Materials.Admin.DeleteMaterial;

public class DeleteMaterialEndpoint(CardinarDbContext context)
    : Endpoint<DeleteMaterialRequest, EmptyResponse>
{
    public override void Configure()
    {
        Delete("v1/admin/materials/delete/{Id:int}");
        Tags("Admin");
        AllowAnonymous();
        Options(x => x.WithTags("Materials"));
    }

    public override async Task<EmptyResponse> ExecuteAsync(DeleteMaterialRequest req, CancellationToken ct)
    {
        var material = await context.Materials.FirstOrDefaultAsync(x => x.Id == req.Id, ct);

        if (material is null)
            throw new Exception("Material not found");

        context.Materials.Remove(material);

        await context.SaveChangesAsync(ct);

        return EmptyResponse.Instance;
    }
}

