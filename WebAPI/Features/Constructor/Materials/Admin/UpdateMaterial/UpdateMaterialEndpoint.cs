using Microsoft.EntityFrameworkCore;

namespace WebAPI.Features.Constructor.Materials.Admin.UpdateMaterial;

public class UpdateMaterialEndpoint(CardinarDbContext context)
    : Endpoint<UpdateMaterialRequest, UpdateMaterialResponse>
{
    public override void Configure()
    {
        Patch("v1/admin/materials/update");
        Tags("Admin");
        AllowAnonymous();
        Options(x => x.WithTags("Materials"));
    }

    public override async Task<UpdateMaterialResponse> ExecuteAsync(UpdateMaterialRequest req, CancellationToken ct)
    {
        var material = await context.Materials.FirstOrDefaultAsync(x => x.Id == req.Id, ct);

        if (material is null)
            throw new Exception("Material not found.");

        var titleExists = await context.Materials
            .AnyAsync(x => x.Title == req.Title && x.Id != req.Id, ct);

        if (titleExists)
            throw new Exception("Material title already exists.");

        material.Title = req.Title;
        material.Description = req.Description;

        await context.SaveChangesAsync(ct);

        return new UpdateMaterialResponse
        {
            Id = material.Id,
            Title = material.Title,
            Description = material.Description
        };
    }
}

