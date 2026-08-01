using Microsoft.EntityFrameworkCore;

namespace WebAPI.Features.Common.SocialLinks.Admin.UpdateSocialLink;

public class UpdateSocialLinkEndpoint(CardinarDbContext context)
    : Endpoint<UpdateSocialLinkRequest, UpdateSocialLinkResponse>
{
    public override void Configure()
    {
        Patch("v1/admin/social-links/update");
        Tags("Admin");
        AllowAnonymous();
        AllowFormData();
        Options(x => x.WithTags("SocialLinks"));
    }

    public override async Task<UpdateSocialLinkResponse> ExecuteAsync(UpdateSocialLinkRequest req, CancellationToken ct)
    {
        var socialLink = await context.SocialLinks.FirstOrDefaultAsync(x => x.Id == req.Id, ct);

        if (socialLink is null)
            throw new Exception("Social link not found.");

        var linkExists = await context.SocialLinks
            .AnyAsync(x => x.Link == req.Link && x.Id != req.Id, ct);

        if (linkExists)
            throw new Exception("Social link URL already exists.");

        socialLink.Title = req.Title;
        socialLink.Link = req.Link;
        
        if (req.Icon != null && req.Icon.Length > 0)
        {
            var dirPath = Path.Combine("uploads");
            Directory.CreateDirectory(dirPath);
            var filePath = Path.Combine(dirPath, req.Icon.FileName);
            await using var file = new FileStream(filePath, FileMode.Create);
            await req.Icon.CopyToAsync(file, ct);
            socialLink.Icon = filePath;
        }

        await context.SaveChangesAsync(ct);

        return new UpdateSocialLinkResponse
        {
            Id = socialLink.Id,
            Title = socialLink.Title,
            Link = socialLink.Link,
            Icon = socialLink.Icon
        };
    }
}

