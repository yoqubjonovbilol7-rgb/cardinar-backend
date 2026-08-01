using Microsoft.EntityFrameworkCore;

namespace WebAPI.Features.Common.SocialLinks.Admin.DeleteSocialLink;

public class DeleteSocialLinkEndpoint(CardinarDbContext context)
    : Endpoint<DeleteSocialLinkRequest, EmptyResponse>
{
    public override void Configure()
    {
        Delete("v1/admin/social-link/delete/{Id:int}");
        Tags("Admin");
        AllowAnonymous();
        Options(x => x.WithTags("SocialLinks"));
    }

    public override async Task<EmptyResponse> ExecuteAsync(DeleteSocialLinkRequest req, CancellationToken ct)
    {
        var socialLink = await context.SocialLinks.FirstOrDefaultAsync(x => x.Id == req.Id, ct);

        if (socialLink is null)
            throw new Exception("Social link not found");

        context.SocialLinks.Remove(socialLink);

        await context.SaveChangesAsync(ct);

        return EmptyResponse.Instance;
    }
}

