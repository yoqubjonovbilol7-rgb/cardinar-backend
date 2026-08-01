using Microsoft.EntityFrameworkCore;

namespace WebAPI.Features.Common.Color.Admin.DeleteColor;

public class DeleteColorEndpoint(CardinarDbContext context)
    : Endpoint<DeleteColorRequest, EmptyResponse>
{
    public override void Configure()
    {
        Delete("v1/admin/colors/delete/{Id:int}");
        Tags("Admin");
        AllowAnonymous();
        Options(x => x.WithTags("Colors"));
    }

    public override async Task<EmptyResponse> ExecuteAsync(DeleteColorRequest req, CancellationToken ct)
    {
        var color = await context.Colors.FirstOrDefaultAsync(x => x.Id == req.Id, ct);

        if (color is null)
            throw new Exception("Color not found");

        context.Colors.Remove(color);

        await context.SaveChangesAsync(ct);

        return EmptyResponse.Instance;
    }
}

