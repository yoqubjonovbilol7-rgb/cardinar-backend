using Microsoft.EntityFrameworkCore;

namespace WebAPI.Features.Common.Color.Admin.UpdateColor;

public class UpdateColorEndpoint(CardinarDbContext context)
    : Endpoint<UpdateColorRequest, UpdateColorResponse>
{
    public override void Configure()
    {
        Patch("v1/admin/colors/update");
        Tags("Admin");
        AllowAnonymous();
        Options(x => x.WithTags("Colors"));
    }

    public override async Task<UpdateColorResponse> ExecuteAsync(UpdateColorRequest req, CancellationToken ct)
    {
        var color = await context.Colors.FirstOrDefaultAsync(x => x.Id == req.Id, ct);

        if (color is null)
            throw new Exception("Color not found.");

        var titleExists = await context.Colors
            .AnyAsync(x => x.Title == req.Title && x.Id != req.Id, ct);

        if (titleExists)
            throw new Exception("Color title already exists.");

        var colorCodeExists = await context.Colors
            .AnyAsync(x => x.ColorCode == req.ColorCode && x.Id != req.Id, ct);

        if (colorCodeExists)
            throw new Exception("Color code already exists.");

        color.Title = req.Title;
        color.ColorCode = req.ColorCode;

        await context.SaveChangesAsync(ct);

        return new UpdateColorResponse
        {
            Id = color.Id,
            Title = color.Title,
            ColorCode = color.ColorCode
        };
    }
}

