using Microsoft.EntityFrameworkCore;

namespace WebAPI.Features.Constructor.Materials.Admin.CreateMaterial;

public class CreateMaterialEndpoint(CardinarDbContext context)
    : Endpoint<CreateMaterialRequest, CreateMaterialResponse>
{
    public override void Configure()
    {
        Post("v1/admin/materials/create");
        Tags("Admin");
        AllowAnonymous();
        Options(x => x.WithTags("Materials"));
    }

    public override async Task<CreateMaterialResponse> ExecuteAsync(CreateMaterialRequest req, CancellationToken ct)
    {
        var titleExists = await context.Materials
            .AnyAsync(x => x.Title == req.Title, ct);

        if (titleExists)
            throw new Exception("Material title already exists.");

        var material = new Entities.Materials
        {
            Title = req.Title,
            Description = req.Description
        };

        context.Materials.Add(material);
        await context.SaveChangesAsync(ct);

        return new CreateMaterialResponse
        {
            Id = material.Id,
            Title = material.Title,
            Description = material.Description
        };
    }
}

