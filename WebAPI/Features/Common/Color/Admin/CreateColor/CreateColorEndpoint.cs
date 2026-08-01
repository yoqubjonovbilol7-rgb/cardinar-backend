using Microsoft.EntityFrameworkCore;

namespace WebAPI.Features.Common.Color.Admin.CreateColor;

public class CreateColorEndpoint(CardinarDbContext context)
    : Endpoint<CreateColorRequest, CreateColorResponse>
{
    public override void Configure()
    {
        Post("v1/admin/colors/create");
        Tags("Admin");
        AllowAnonymous();
        Options(x => x.WithTags("Colors"));
    }

    public override async Task<CreateColorResponse> ExecuteAsync(CreateColorRequest req, CancellationToken ct)
    {
        var titleExists = await context.Colors
            .AnyAsync(x => x.Title == req.Title, ct);

        if (titleExists)
            throw new Exception("Color title already exists.");

        var colorCodeExists = await context.Colors
            .AnyAsync(x => x.ColorCode == req.ColorCode, ct);

        if (colorCodeExists)
            throw new Exception("Color code already exists.");

        var color = new Entities.Color
        {
            Title = req.Title,
            ColorCode = req.ColorCode
        };

        context.Colors.Add(color);
        await context.SaveChangesAsync(ct);

        return new CreateColorResponse
        {
            Id = color.Id,
            Title = color.Title,
            ColorCode = color.ColorCode
        };
    }
}

