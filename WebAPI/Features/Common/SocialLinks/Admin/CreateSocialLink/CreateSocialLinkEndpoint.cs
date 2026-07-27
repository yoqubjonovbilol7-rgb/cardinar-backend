using WebAPI.Features.Common.Entities;

namespace WebAPI.Features.Common.SocialLinks.Admin.CreateSocialLink;

public class CreateSocialLinkEndpoint(CardinarDbContext ctx) : Endpoint<CreateSocialLinkRequest, SocialLink>
{
  public override void Configure()
  {
    Post("v1/admin/social-links/create");
    Policies("Admin");
    Tags("Admin");
    Options(opts => opts.WithTags("SocialLink"));
    AllowFormData();
  }

  public override async Task<SocialLink> ExecuteAsync(CreateSocialLinkRequest req, CancellationToken ct)
  {
    var dirPath = Path.Combine("uploads");
    Directory.CreateDirectory(dirPath);
    var filePath = Path.Combine(dirPath, req.Icon.FileName);
    await using var file = new FileStream(filePath, FileMode.Create);
    await req.Icon.CopyToAsync(file, ct);
    
    var newLink = req.ToEntity(filePath);
    ctx.SocialLinks.Add(newLink);
    await ctx.SaveChangesAsync(ct);
    return newLink;
  }
}